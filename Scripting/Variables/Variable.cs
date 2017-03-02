using System;

using UnityEngine;

namespace Scripting
{
    [Serializable]
    public class Variable
    {
        const char VARIABLE_IDENTIFIER = '$';

        public string Identifier
        {
            get { return identifier; }
        }

        [SerializeField]
        private string identifier;

        public string Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }
        
        [SerializeField]
        private string value;

        [SerializeField]
        private Query query;

        public static bool IsValidIdentifier(string identifier)
        {
            return !String.IsNullOrEmpty(identifier) && identifier[0] == VARIABLE_IDENTIFIER;
        }
    }
}
