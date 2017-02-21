using UnityEngine;
using System;

namespace Scripting
{   
    [Serializable]
    public class ScriptObjectQuery
    {
        [SerializeField] private string contentId;

        public string ResolvedString
        {
            get
            {
                return resolvedString;
            }
        }

        private string resolvedString;

        [SerializeField]
        private ScriptFieldQuery fieldQuery;

        public void OnAfterDeserialize()
        {
            fieldQuery.Resolve(contentId);
        }
    }
}
