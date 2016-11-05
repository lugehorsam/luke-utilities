using System;
using System.Linq;
using System.Collections.Generic;

public static class IListExtensions {

    public static List<int> MapIndices<T> (this IList<T> thisIList, IList<T> otherList)
    {
        return thisIList.Select ((item) => otherList.IndexOf (item)).ToList ();
    }

    public static void InsertOrAdd<T>(this IList<T> thisList, int insertionIndex, T item)
    {
        if (insertionIndex > thisList.Count) {
            Diagnostics.Report (new IndexOutOfRangeException("insertion index is " + insertionIndex));
            return;
        }
        else if (insertionIndex == thisList.Count) {
            thisList.Add (item);
        } else {
            thisList.Insert (insertionIndex, item);
        }
    }

}
