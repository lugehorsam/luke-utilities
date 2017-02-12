using UnityEngine;
using System;

namespace Scripting
{
    [Serializable]
    public class TextQuery
    {
        public string ContentId
        {
            get { return contentId; }
        }

        [SerializeField] private string contentId;

        public string Query
        {
            get { return query; }
        }

        [SerializeField] private string query;

        public override string ToString()
        {
            return "";
        }
    }
}
