using System;
using System.Collections;
using System.Collections.Generic;
using Datum;
using System;

namespace Scripting
{
    public static class ScriptManager
    {
        private static readonly Dictionary<string, ScriptObject[]> contentLists =
            new Dictionary<string, ScriptObject[]>();

        public static IEnumerator FetchContent<TDatum>(string path,
            DatumRequestType requestType = DatumRequestType.Local)
            where TDatum : ScriptObject
        {
            var request = requestType.ToRequest<ContentList<TDatum>>(path);
            yield return request;
            ContentList<TDatum> content = request.Datum;
            contentLists[request.Datum.Id] = content.Array;
            RegisterGlobals(content.Globals);
        }

        static void RegisterGlobals(Variable[] globals)
        {
            foreach (var global in globals)
            {
                if (Variable.IsValidIdentifier(global.Identifier))
                {
                    Variable.AddVariable(global);
                }
                else
                {
                    throw new FormatException(string.Format("Variable identifier {0} is malformed", global.Identifier));
                }
            }
        }
    }
}
