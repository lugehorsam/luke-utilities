using System.Collections.Generic;

public static class ListExtensions {

    public static T GetLast<T>(this List<T> thisList)
    {
        return thisList[thisList.Count - 1];
    }
}
