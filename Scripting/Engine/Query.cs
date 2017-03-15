using UnityEngine;
using System;
using System.Reflection;

namespace Scripting
{   
    [Serializable]
    public class Query : IRuntimeResolvable
    {
        public bool HasResolved
        {
            get;
            private set;
        }

        [SerializeField] private string table;
        [SerializeField] private string property;
        [SerializeField] private string value;

        public ScriptObject GetResolvedValue(ScriptRuntime runtime)
        {
            var objectsToSearch = runtime.GetScriptObjects(table);

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
                            return val;
                        }
                    }
                }
            }

            return null;
        }
    }
}
