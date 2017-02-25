using System;
using UnityEngine;

namespace Scripting
{
    [Serializable]
    public abstract class TextDatum : ScriptObject, ISerializationCallbackReceiver
    {
        public string Text
        {
            get { return string.Format(text, resolvedQueries); }
        }

        [SerializeField] private string text;
        [SerializeField] private MatchQuery[] args = {};

        private string[] resolvedQueries;

        public void OnAfterDeserialize()
        {
            resolvedQueries = new string[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                resolvedQueries[i] = args[i].ResolvedString;
            }
        }

        public void OnBeforeSerialize()
        {
            
        }
    }
}
