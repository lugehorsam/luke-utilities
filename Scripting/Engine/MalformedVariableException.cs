using System;

namespace Scripting
{
    public class MalformedVariableException : FormatException {

        public MalformedVariableException(string identifier) : base(string.Format("Variable identifier {0} is malformed", identifier))
        {
        }
    }
}

