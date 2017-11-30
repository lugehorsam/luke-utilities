namespace Utilities
{
    using System.Collections.Generic;

    public static class ListExt
    {
        public static T GetLast<T>(this List<T> thisList)
        {
            return thisList[thisList.Count - 1];
        }
    }
}
