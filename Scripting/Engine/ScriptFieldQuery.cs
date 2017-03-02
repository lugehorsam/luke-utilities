using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Scripting
{
    [Serializable]    
    public class ScriptFieldQuery
    {       
        [SerializeField]
        private string property;
        
        [SerializeField]
        private string value;

        public ScriptObject GetMatchingObject(string tableId, ScriptRuntime runtime)
        {
            runtime.TryResolveValue(value, out value);
            
            foreach (ScriptObject scriptObject in runtime.ScriptObjects[tableId])
            {
                FieldInfo fieldWithPropertyName = ReflectionUtils.GetNonDefaultFields(scriptObject)
                    .FirstOrDefault((field) => field.Name == property);


                if (fieldWithPropertyName.GetValue(scriptObject) == value)
                {
                    return scriptObject;
                }
            }
        }
    }
}
