using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Scripting
{
    [Serializable]
    public class ScriptTable<TDatum> : JsonArray<TDatum>
    {        
        public string Id => id;

        [SerializeField] private string id;

        public Variable[] Globals => globals;

        [SerializeField] private Variable[] globals;                
    }
}
