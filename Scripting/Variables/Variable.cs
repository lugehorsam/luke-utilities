using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

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

        public static T GetValue<T>(string variable) where T : ScriptObject<T>
        {
            return ScriptObject<T>.Collection.FirstOrDefault(
                (scriptObject) => scriptObject.Id == GetValue(variable)
            ) as T;
        }

        public static string GetValue(string variable)
        {
            return variables[variable];
        }

        public static void SetValue(string variable, string value)
        {
            if (variable[0] != VARIABLE_IDENTIFIER)
            {
                throw new Exception();
            }

            variables[variable] = value;
        }
    }
}
