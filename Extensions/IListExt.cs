namespace Utilities
{
    using System.Collections.Generic;
    using System.Linq;

    public static class IListExt
    {
        public static List<int> GetItemIndicesIn<T>(this IList<T> thisIList, IList<T> otherList)
        {
            return thisIList.Select(item => otherList.IndexOf(item)).ToList();
        }

        public static void Replace<T>(this IList<T> thisList, T oldItem, T newItem)
        {
            int oldItemIndex = thisList.IndexOf(oldItem);
            thisList[oldItemIndex] = newItem;
        }

        /// <summary>
        ///     No duplicates in a row.
        /// </summary>
        /// <returns></returns>
        public static List<T> DistinctSequence<T>(this IList<T> thisList)
        {
            var newList = new List<T>();

            for (var i = 0; i < thisList.Count; i++)
            {
                T currentItem = thisList[i];

                if ((i == 0) || !currentItem.Equals(thisList[i - 1]))
                {
                    newList.Add(currentItem);
                }
            }

            return newList;
        }
    }
}
