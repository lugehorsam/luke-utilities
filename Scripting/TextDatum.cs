using System;
using UnityEngine;
using System.Collections.Generic;

namespace Scripting
{
    [Serializable]
    public class TextDatum : ScriptObject, ISerializationCallbackReceiver
    {
        public string Text
        {
            get { return string.Format(text, resolvedQueries); }
        }

        [SerializeField] private string text;
        [SerializeField] private TextQuery[] args;

        private string[] resolvedQueries;

        public void OnAfterDeserialize()
        {
            resolvedQueries = new string[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                resolvedQueries[i] = args[i].Resolve();
            }
        }

        public void OnBeforeSerialize()
        {

        }
    }
}
