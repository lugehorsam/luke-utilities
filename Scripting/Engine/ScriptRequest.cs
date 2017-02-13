using System.Diagnostics;
using Datum;

namespace Scripting
{
    public class ScriptRequest<TDatum> : DatumRequest<ContentList<TDatum>> where TDatum : ScriptObject
    {
        private readonly DatumRequest<ContentList<TDatum>> request;

        public ScriptRequest(ScriptContentConfig<TDatum> config, DatumRequestType requestType)
        {
            request = requestType.ToRequest<ContentList<TDatum>>(config.ResourcesPath);
        }

        public override bool RequestIsDone()
        {
            return !request.keepWaiting;
        }

        protected override void HandleAfterDeserialize(string rawContent)
        {
            ContentList<TDatum> content = request.Datum;
            Diagnostics.Log("content is " + content);
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
