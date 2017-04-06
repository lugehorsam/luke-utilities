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

    /// <summary>
    /// No duplicates in a row.
    /// </summary>
    /// <returns></returns>
    public static List<T> DistinctSequence<T>(this IList<T> thisList)
    {
        List<T> newList = new List<T>();

        for (int i = 0; i < thisList.Count; i++)
        {
            T currentItem = thisList[i];

            if (i == 0 || !currentItem.Equals(thisList[i - 1]))
            {
                newList.Add(currentItem);
            }
        }

        return newList;
    }
}
