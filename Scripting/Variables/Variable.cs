using System;

using UnityEngine;

namespace Scripting
{
    [Serializable]
    public class Variable : IRuntimeResolvable
    {
        const char VARIABLE_IDENTIFIER = '$';

        public string Identifier => identifier;

        [SerializeField]
        private string identifier;

        [SerializeField]
        private string value;

        [SerializeField]
        private Query query;

        public static bool IsValidIdentifier(string identifier)
        {
            return !String.IsNullOrEmpty(identifier) && identifier[0] == VARIABLE_IDENTIFIER;
        }

        public ScriptObject GetResolvedValue(ScriptRuntime runtime)
        {
            foreach (IRuntimeResolvable resolvable in GetRuntimeResolvables())
            {
                if (resolvable != null)
                {
                    return resolvable.GetResolvedValue(runtime);
                }
            }

            if (IsValidIdentifier(value))
            {
                Variable referencedVariable = runtime.GetVariableWithIdentifier(value);
                return referencedVariable.GetResolvedValue(runtime);
            }

            return runtime.GetScriptObject(value);
        }

        IRuntimeResolvable[] GetRuntimeResolvables()
        {
            return new[]{query};
        }
    }
}
