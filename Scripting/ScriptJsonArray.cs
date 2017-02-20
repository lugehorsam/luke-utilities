using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Scripting
{
    [Serializable]
    public class ScriptJsonArray<TDatum> : JsonArray<TDatum>
    {        
        public string Id
        {
            get { return id; }
        }
        
        [SerializeField] private string id;

        public Variable[] Globals
        {
            get { return globals; }
        }

        [SerializeField] private Variable[] globals;

    }
}
