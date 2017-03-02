using System.Collections.Generic;
using UnityEngine;

namespace Scripting
{
    public abstract class ScriptObject
    {

        protected ScriptRuntime ScriptRuntime
        {
            get;
            private set;
        }

        public string Id
        {
            get { return id; }
        }

        [SerializeField] private string id;

        public Condition ShouldAppear
        {
            get { return shouldAppear; }
        }

        [SerializeField] private Condition shouldAppear;

        public string Display
        {
            get { return display ?? id.ToUpper(); }
        }

        [SerializeField] private string display;

        public abstract string ResourcesPath
        {
            get;
        }

        public void RegisterRuntime(ScriptRuntime runtime)
        {
            ScriptRuntime = runtime;
            OnAfterRegisterRuntime();
        }

        protected virtual void OnAfterRegisterRuntime() { }
    }
}
