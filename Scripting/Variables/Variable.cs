using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Scripting
{
    public class Variable
    {
        static readonly Dictionary<string, string> variables = new Dictionary<string, string>();

        const char VARIABLE_IDENTIFIER = '$';

        public string Name
        {
            get { return name; }
        }

        private string name;
        private string value;

        public static bool IsVariable(string varName)
        {
            return varName.Length > 0 && varName[0] == VARIABLE_IDENTIFIER;
        }

        public static string GetValue(string variable)
        {
            return variables[variable];
        }
    }
}
