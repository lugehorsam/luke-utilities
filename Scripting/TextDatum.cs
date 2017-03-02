using System;
using UnityEngine;

namespace Scripting
{
    [Serializable]
    public abstract class TextDatum : ScriptObject
    {
        public string Text
        {
            get { return string.Format(text, resolvedQueries); }
        }

        [SerializeField] private string text;
        [SerializeField] private string[] args;

        private string[] resolvedQueries;

        protected override void OnAfterRegisterRuntime()
        {
            for (int i = 0; i < args.Length; i++)
            {
                string newVal;
                if (ScriptRuntime.TryResolveValue(args[i], out newVal))
                {
                    args[i] = newVal;
                }
                else
                {
                    throw new Exception(
                        string.Format("Couldn't resolve arg {0} in text {1} ", args[i], text)
                    );
                }
            }
        }
    }
}
