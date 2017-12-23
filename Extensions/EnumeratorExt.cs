namespace Utilities
{
    using System.Collections;
    using System.Collections.Generic;

    public static class EnumeratorExt
    {
        public static void Run(this IEnumerator thisEnumerator)
        {
            while (thisEnumerator.MoveNext()) { }
        }

        public static List<T> Run<T>(this IEnumerator<T> thisEnumerator)
        {
            var objects = new List<T>();

            while (thisEnumerator.MoveNext())
            {
                objects.Add(thisEnumerator.Current);
            }

            return objects;
        }
    }
}
