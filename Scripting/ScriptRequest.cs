using Datum;
using UnityEngine;

namespace Scripting
{
    public class ScriptRequest<TDatum> : DatumRequest<ContentList<TDatum>> where TDatum : ScriptObject
    {
        private const string CONTENT_CONFIG_PATH = "ContentConfigs/";

        private readonly DatumRequest<ContentList<TDatum>> request;

        private readonly LazyReference<ScriptContentConfig> contentConfig;

        public ScriptRequest(DatumRequestType requestType)
        {
            contentConfig = new LazyReference<ScriptContentConfig>(() =>
                Resources.Load<ScriptContentConfig<TDatum>>(CONTENT_CONFIG_PATH));

            request = requestType.ToRequest<ContentList<TDatum>>(contentConfig.Value.ResourcesPath);
        }

        public override bool RequestIsDone()
        {
            return request.RequestIsDone();
        }

        protected override void HandleAfterDeserialize(string rawContent)
        {
            ContentList<TDatum> content = request.Datum;

            foreach (var global in content.Globals)
            {
                if (Variable.IsValidIdentifier(global.Identifier))
                {
                    Variable.AddVariable(global);
                }
                else
                {
                    throw new MalformedVariableException(global.Identifier);
                }
            }
        }

        public override string GetRawContent()
        {
            return request.GetRawContent();
        }
    }
}