using System.Linq;
using System.Collections.Generic;

public static class IListExtensions {

    public static List<int> GetItemIndicesIn<T> (this IList<T> thisIList, IList<T> otherList)
    {
        return thisIList.Select ((item) => otherList.IndexOf (item)).ToList ();
    }

    public static void Replace<T>(this IList<T> thisList, T oldItem, T newItem)
    {
        var oldItemIndex = thisList.IndexOf(oldItem);
        thisList[oldItemIndex] = newItem;
    }
}
