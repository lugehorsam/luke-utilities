namespace Utilities
{
    using System;

    public static class StringExt
    {
        public static T ToEnum<T>(this string thisString)
        {
            if (string.IsNullOrEmpty(thisString))
            {
                return default(T);
            }

            return (T) Enum.Parse(typeof(T), thisString);
        }
    }
}
