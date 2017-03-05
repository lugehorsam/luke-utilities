using System;

namespace Scripting
{
    public class InvalidIdentifierException : FormatException {

        public string Identifier
        {
            get;
            set;
        }
        
        public InvalidIdentifierException(string identifier, string tableId = "", int tableIndex = 0, string value = "") : base(string.Format("Variable identifier {0} is malformed in table {1} at index {2} and value {3}", identifier, tableId, tableIndex, value))
        {
            Identifier = identifier;
        }
    }
}
