using System.Collections;
using Datum;

namespace Scripting
{

    public class ScriptRequest<TDatum> : DatumRequest<ContentList<TDatum>> where TDatum : ScriptObject
    {
        private readonly DatumRequest<ContentList<TDatum>> request;

        public ScriptRequest(string path, DatumRequestType requestType)
        {
            request = requestType.ToRequest<ContentList<TDatum>>(path);
        }

        public override bool RequestIsDone()
        {
            return request.RequestIsDone();
        }

        protected override void HandleAfterDeserialize(string rawContent)
        {
            ContentList<TDatum> content = request.Datum;
            if (content != null)
            {
                ScriptManager.ContentLists[request.Datum.Id] = content.Array;
                if (content.Globals != null)
                    ScriptManager.AddGlobals(content.Globals);
            }
        }

        public override string GetRawContent()
        {
            return request.GetRawContent();
        }
    }
}