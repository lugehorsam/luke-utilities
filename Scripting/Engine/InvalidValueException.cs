using System;

namespace Utilities.Scripting
{
    public class InvalidValueException : Exception
    {
        public InvalidValueException(Variable variable) : base($"Invalid variable value for {variable}")
        {

        }
    }
}
