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

        public static bool IsValidIdentifier(string varName)
        {
            return varName.Length > 0 && varName[0] == VARIABLE_IDENTIFIER;
        }
       
    }
}
