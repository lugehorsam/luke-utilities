using System;
using System.Linq;
using System.Collections.Generic;

public static class IListExtensions {

    public static List<int> MapIndices<T> (this IList<T> thisIList, IList<T> otherList)
    {
        return thisIList.Select ((item) => otherList.IndexOf (item)).ToList ();
    }
}
