using System.Collections.Generic;
using Datum;

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

            for (int i = 0; i < content.Globals.Length; i++) {
                Variable global = content.Globals[i];
                try
                {
                    scriptRuntime.AddVariable(global);
                }
                catch (InvalidIdentifierException e)
                {
                    throw new InvalidIdentifierException(e.Identifier, content.Id);
                }
            }
        }

        void AddScriptObjects(ScriptTable<TDatum> content)
        {
            if (!scriptRuntime.ScriptObjects.ContainsKey(content.Id))
                scriptRuntime.ScriptObjects[content.Id] = new List<ScriptObject>();
            
            foreach (var item in content.Array)
            {
                scriptRuntime.ScriptObjects[content.Id].Add(item);
            }
        }
    }
}
