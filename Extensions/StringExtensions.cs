using System;

public static class StringExtensions {
    
    public static T ToEnum<T>(this string thisString)
    {
        return (T) Enum.Parse(typeof(T), thisString);
    }
}
