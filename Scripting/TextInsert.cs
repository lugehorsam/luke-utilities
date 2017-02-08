using UnityEngine;
using System;
using System.Collections.Generic;

namespace Scripting
{
    [Serializable]
    public class TextInsert
    {
        [SerializeField] private string source;
        [SerializeField] private string query;

        public override string ToString()
        {
            return "";
        }
    }
}
