using System.Collections.Generic;

public static class EnumerableExtensions {

    public static string ToFormattedString<T>(this IEnumerable<T> thisEnumerable) {
        if (thisEnumerable == null) {

            return "";
        }

        string formattedString = "";
        foreach (T item in thisEnumerable) {
            formattedString += (item.ToString() + ", ");
        }
        return formattedString;
    }

  
}
