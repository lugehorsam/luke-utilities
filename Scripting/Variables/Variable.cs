using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripting
{
    [Serializable]
    public class Variable : ISerializationCallbackReceiver
    {
        private static readonly HashSet<Variable> variables = new HashSet<Variable>();
        private static readonly HashSet<Variable> unresolvedVariables = new HashSet<Variable>();

        const char VARIABLE_IDENTIFIER = '$';

        public string Identifier
        {
            get { return identifier; }
        }

        [SerializeField]
        private string identifier;

        [SerializeField]
        private string value;

        public static bool IsValidIdentifier(string varName)
        {
            return varName.Length > 0 && varName[0] == VARIABLE_IDENTIFIER;
        }

        public static void AddVariable(Variable variable)
        {
            if (!IsValidIdentifier(variable.identifier))
            {
                throw new MalformedVariableException(variable.identifier);
            }
        }

        static Variable GetVariableWithIdentifier(string identifier)
        {
            if (!IsValidIdentifier(identifier))
            {
                throw new MalformedVariableException(identifier);
            }

            return variables.FirstOrDefault((variable) => variable.identifier == identifier);
        }

        public void OnAfterDeserialize()
        {
            if (!IsValidIdentifier(identifier))
            {
                throw new MalformedVariableException(identifier);
            }

            if (TryResolveValue(value, out value))
            {
                variables.Add(this);
            }
            else
            {
                unresolvedVariables.Add(this);
            }
        }

        void TryResolveVariables(Variable newVariable)
        {
            foreach (var variable in unresolvedVariables)
            {
                if (variable.value == newVariable.identifier)
                {
                    variable.value = newVariable.value;
                }
            }
        }

        public void OnBeforeSerialize()
        {

        }

        bool TryResolveValue(string rawValue, out string resolvedValue)
        {
            if (IsValidIdentifier(rawValue))
            {
                var identifiedVar = GetVariableWithIdentifier(rawValue);

                if (identifiedVar == null)
                {
                    resolvedValue = rawValue;
                    return false;
                }

                resolvedValue = identifiedVar.value;
                return true;
            }

            resolvedValue = rawValue;
            return true;
        }
    }
}
