using System;
using UnityEngine;

namespace Scripting
{

    [Serializable]
    public class Condition
    {
        [SerializeField] private string left;
        [SerializeField] private string right;
        [SerializeField] private string comparison;

        public bool Evaluate()
        {
            TryResolveVariable(left, out left);
            TryResolveVariable(right, out right);

            int leftAsInt;
            int rightAsInt;

            if (ArgsAreInts(out leftAsInt, out rightAsInt))
            {
                return Evaluate(leftAsInt, rightAsInt);
            }

            return Evaluate(left, right);
        }

        bool Evaluate<T>(T left, T right) where T : IComparable<T> {
            switch (comparison) {
                case "<": return left.CompareTo(right) < 0;
                case ">": return left.CompareTo(right) > 0;
                case "<=": return left.CompareTo(right) <= 0;
                case ">=": return left.CompareTo(right) >= 0;
                case "==": return left.Equals(right);
                case "!=": return !left.Equals(right);
                default: throw new ArgumentException("Invalid comparison operator: {0}", comparison);
            }
        }

        bool ArgsAreInts(out int leftAsInt, out int rightAsInt)
        {
            leftAsInt = 0;
            rightAsInt = 0;
            return int.TryParse(left, out leftAsInt) && int.TryParse(right, out rightAsInt);
        }

        bool TryResolveVariable(string variable, out string value)
        {
            value = "";
            if (Variable.IsValidIdentifier(variable))
            {
                //value = Variable.GetValue(variable);
                return true;
            }

            value = variable;
            return false;
        }
    }
}