using System;

using UnityEngine;

namespace Scripting
{
    [Serializable]
    public class Variable : ISerializationCallbackReceiver
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

        public Query Query
        {
            get
            {
                return query;
            }
        }

        [SerializeField]
        private Query query;

        public static bool IsValidIdentifier(string identifier)
        {
            return !String.IsNullOrEmpty(identifier) && identifier[0] == VARIABLE_IDENTIFIER;
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            if (query == null && value == null)
                Diagnostics.Report("Variable {0} serialized with no raw value or query", this.ToString());
        }

        public override string ToString()
        {
            return string.Format("[Variable: Identifier={0}, Value={1}, Query ={2}]", Identifier, Value, query);
        }
    }
}
