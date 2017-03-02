using UnityEngine;
using System;
using System.Reflection;

namespace Scripting
{   
    [Serializable]
    public class Query
    {
        [SerializeField] private string table;
        [SerializeField] private string property;
        [SerializeField] private string value;

        public bool TryGetResolvedString(ScriptRuntime runtime, out string resolvedString)
        {
            resolvedString = null;
            var objectsToSearch = runtime.ScriptObjects[table];

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
                            resolvedString = val.Display;
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
