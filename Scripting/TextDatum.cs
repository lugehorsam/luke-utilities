using System;
using UnityEngine;

namespace Utilities.Scripting
{
    [Serializable]
    public abstract class TextDatum : ScriptObject
    {
        public string Text => resolvedQueries == null ? text : string.Format(text, resolvedQueries);

        [SerializeField] private string text;
        [SerializeField] private string[] args;

        private string[] resolvedQueries;

        protected override void OnAfterRegisterRuntime()
        {
            if (args == null)
                return;

            resolvedQueries = new string[args.Length];
            
            for (int i = 0; i < args.Length; i++)
            {
                ScriptObject scriptObj = ScriptRuntime.GetScriptObject(args[i]);
//                resolvedQueries[i] = scriptObj.Display;
            }
        }
    }
}
