using System;
using UnityEngine;

namespace Scripting
{
    [Serializable]
    public class ContentList<TDatum> : JsonArray<TDatum>
    {
        public string Id
        {
            get { return id; }
        }
        [SerializeField]
        private string id;

        public Variable[] Globals
        {
            get { return globals; }
        }

        [SerializeField] private Variable[] globals = { };
    }
}
