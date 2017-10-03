using System.Collections.Generic;
/**
namespace Scripting
{
    public class LocalScriptRequest<TDatum> : ResourcesRequest<ScriptTable<TDatum>>
        where TDatum : ScriptObject, new()
    {
        private readonly ScriptRuntime scriptRuntime;

        public LocalScriptRequest(ScriptRuntime scriptRuntime) : base(new TDatum().ResourcesPath)
        {
            this.scriptRuntime = scriptRuntime;
        }

        protected override void HandleAfterDeserialize()
        {
            AddGlobals(Datum);
            AddScriptObjects(Datum); 
        }

        void AddGlobals(ScriptTable<TDatum> content)
        {
            if (content.Globals == null)
            {
                return;
            }

            for (int i = 0; i < content.Globals.Length; i++) 
            {
                Variable global = content.Globals[i];
                scriptRuntime.AddVariable(global);
            }
        }

        void AddScriptObjects(ScriptTable<TDatum> content)
        {   
            Diagnostics.Log("Adding script objects for table: {0}", content.Id);
            foreach (var item in content.Array)
            {
                scriptRuntime.AddScriptObject(content.Id, item);
            }
        }
    }
}
**/