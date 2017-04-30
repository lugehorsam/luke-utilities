using System;

public static class StringExtensions {
    
    public static T ToEnum<T>(this string thisString)
    {
        if (String.IsNullOrEmpty(thisString))
        {
            return default(T);
        }
        
        return (T) Enum.Parse(typeof(T), thisString);
    }
}
