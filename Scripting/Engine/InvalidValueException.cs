using System;

namespace Utilities.Scripting
{
    public class InvalidValueException : Exception
    {
        public InvalidValueException(Variable variable) : base(string.Format("Invalid variable value for {0}", variable))
        {

        }
    }
}
