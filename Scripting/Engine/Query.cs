using UnityEngine;
using System;
using System.Reflection;

namespace Scripting
{   
    [Serializable]
    public class Query
    {
        [SerializeField] private string property;
        [SerializeField] private string value;

        public string GetResolvedString(ScriptRuntime runtime, string tableId)
        {
            var objectsToSearch = runtime.ScriptObjects[tableId];

            foreach (ScriptObject scriptObject in objectsToSearch)
            {
                FieldInfo[] fields = scriptObject.GetType().GetFields();
                foreach (var field in fields)
                {
                    if (field.Name == property)
                    {
                        var val = field.GetValue(scriptObject) as ScriptObject;
                        if (val.Id == value)
                        {
                            return val.Display;
                        }
                    }
                }
            }
            return "";
        }
    }
}
