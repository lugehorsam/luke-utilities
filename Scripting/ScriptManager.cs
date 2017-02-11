using System;
using System.Collections;
using System.Collections.Generic;
using Datum;
using System;

namespace Scripting
{
    public static class ScriptManager
    {

        public static Dictionary<string, ScriptObject[]> ContentLists
        {
            get { return contentLists; }
        }

        private static readonly Dictionary<string, ScriptObject[]> contentLists =
            new Dictionary<string, ScriptObject[]>();

        public static void AddGlobals(Variable[] globals)
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
