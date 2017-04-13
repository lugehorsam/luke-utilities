using System.Collections.Generic;
using System.Linq;

public static class EnumerableExtensions {

    public static string ToFormattedString<T>(this IEnumerable<T> thisEnumerable) {
        if (thisEnumerable == null) {

            return "";
        }

        string formattedString = "";
        foreach (T item in thisEnumerable) {
            formattedString += (item);

            if (!item.Equals(thisEnumerable.LastOrDefault()))
                formattedString += ", ";
        }
        return formattedString;
    }

  
}
