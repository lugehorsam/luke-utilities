using System;
using System.Collections.Generic;
using System.Linq;
using Datum;

namespace Scripting
{
    public class ScriptRequest<TDatum> : DatumRequest<ScriptJsonArray<TDatum>>
        where TDatum : ScriptObject, new()
    {
        private readonly DatumRequest<ScriptJsonArray<TDatum>> request;
        private readonly ScriptRuntime scriptRuntime;

        public ScriptRequest(DatumRequestType requestType, ScriptRuntime scriptRuntime)
        {
            string resourcesPath = new TDatum().ResourcesPath;
            request = requestType.ToRequest<ScriptJsonArray<TDatum>>(resourcesPath);
            this.scriptRuntime = scriptRuntime;
        }

        public override bool RequestIsDone()
        {
            return !request.keepWaiting;
        }

        protected override void HandleAfterDeserialize(string rawContent)
        {
            ScriptJsonArray<TDatum> content = request.Datum;

            if (content == null)
            {
                throw new NullReferenceException(string.Format("Request {0} has a null array", rawContent));
            }
            
            AddGlobals(content);
            AddScriptObjects(content); 
        }

        void AddGlobals(ScriptJsonArray<TDatum> content)
        {
            if (content.Globals == null)
            {
                return;
            }
            
            foreach (var global in content.Globals)
            {
                if (Variable.IsValidIdentifier(global.Identifier))
                {
                    scriptRuntime.AddVariable(global);
                }
                else
                {
                    throw new MalformedVariableException(global.Identifier);
                }
            }

        }

        void AddScriptObjects(ScriptJsonArray<TDatum> content)
        {
            if (!scriptRuntime.ScriptObjects.ContainsKey(content.Id))
                scriptRuntime.ScriptObjects[content.Id] = new List<ScriptObject>();
            
            foreach (var item in content.Array)
            {
                scriptRuntime.ScriptObjects[content.Id].Add(item);
            }
        }

        public override string GetRawContent()
        {
            return request.GetRawContent();
        }
    }
}
