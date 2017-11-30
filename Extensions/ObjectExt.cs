namespace Utilities
{
    public static class ObjectExt
    {
        public static string ToString(this object thisObject, params object[] members)
        {
            var result = "[";

            for (var i = 0; i < members.Length; i++)
            {
                object member = members[i];
                result += member == null ? "(null)" : member.ToString();
                if (i < members.Length - 1)
                {
                    result += ", ";
                }
            }

            result += "]";

            return result;
        }
    }
}
