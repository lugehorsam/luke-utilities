using System.Collections;
using System.Collections.Generic;
using Datum;

namespace Scripting
{
    public static class ScriptManager
    {
        private static readonly Dictionary<string, ScriptObject[]> contentLists = new Dictionary<string, ScriptObject[]>();

        public static IEnumerator FetchContent<TDatum>(string path, DatumRequestType requestType = DatumRequestType.Local)
            where TDatum : ScriptObject
        {
            switch (requestType)
            {
                case DatumRequestType.Local:
                    var request = new ResourcesRequest<ContentList<TDatum>>(path);
                    yield return request;
                    contentLists[request.Datum.Id] = request.Datum.Array;
                    break;
            }
        }
    }
}
