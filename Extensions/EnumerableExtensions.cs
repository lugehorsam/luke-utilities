using System;
using System.Linq;
using System.Collections.Generic;

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

    public static T GetRandomElement<T>(this IEnumerable<T> thisEnumerable)
    {
        int randNum = Randomizer.Randomize(0, thisEnumerable.Count() - 1);

        int counter = 0;
        foreach (var item in thisEnumerable)
        {
            if (counter == randNum)
            {
                return item;
            }
            counter++;
        }

        return default(T);
    }     
}
