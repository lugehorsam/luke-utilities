using System;

namespace Scripting
{
    public class InvalidIdentifierException : FormatException {

        public string Identifier
        {
            get;
            set;
        }
        
        public InvalidIdentifierException(string identifier) : base(string.Format("Variable identifier {0} is malformed", identifier))
        {
            Identifier = identifier;
        }

        public InvalidIdentifierException(string identifier, string tableId, int tableIndex) : base(string.Format("Variable identifier {0} is malformed in table {1} at index {2}", identifier, tableId, tableIndex))
        {
            Identifier = identifier;
        }
    }
}
