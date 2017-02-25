using UnityEngine;
using System;

namespace Scripting
{   
    [Serializable]
    public class MatchQuery
    {
        [SerializeField] private string from;
        [SerializeField] private string where;
        [SerializeField] private string equals;

        public string GetResolvedString(ScriptRuntime runtime)
        {
            var objectsToSearch = runtime.ScriptObjects[from];

            foreach (ScriptObject scriptObject in objectsToSearch)
            {
                    
            }
        }
    }
}
