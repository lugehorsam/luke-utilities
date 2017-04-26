using System;

public static class ObjectExtensions
{
    public static string ToString(this Object thisObject, params object[] members)
    {
        string result = "[";

        for (int i = 0; i < members.Length; i++)
        {
            object member = members[i];
            result += member == null ? "(null)" : member.ToString();
            if (i < members.Length - 1)
                result += ", ";
        }

        result += "]";

        return result;
    }
}
