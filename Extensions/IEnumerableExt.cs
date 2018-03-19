namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Random = UnityEngine.Random;

    public static class IEnumerableExt
    {
        public static string PrettyPrint<T>(this IEnumerable<T> thisEnumerable)
        {
            if (thisEnumerable == null)
            {
                return "";
            }

            var formattedString = "";

            var i = 0;

            foreach (T item in thisEnumerable)
            {
                if (item == null)
                {
                    continue;
                }

                formattedString += item.ToString();

                if (!item.Equals(thisEnumerable.LastOrDefault()))
                {
                    formattedString += $"({i}) ";
                    var formatter = ", ";

                    if (item is KeyValuePair<object, object>)
                    {
                        formatter = ": ";
                    }

                    formattedString += formatter;
                }
            }

            i++;
            return formattedString;
        }

        public static T GetRandomElement<T>(this IEnumerable<T> thisEnumerable)
        {
            int randNum = Random.Range(0, thisEnumerable.Count());

            var counter = 0;

            foreach (T item in thisEnumerable)
            {
                if (counter == randNum)
                {
                    return item;
                }

                counter++;
            }

            return default(T);
        }

        public static List<T> ToGenericBase<TGeneric, T>(TGeneric genericEnumerable) where TGeneric : IEnumerable<T>
        {
            var baseEnumerable = new List<T>();
            foreach (T item in genericEnumerable)
            {
                baseEnumerable.Add(item);
            }

            return baseEnumerable;
        }

        public static bool IdenticalContent<T>(this IEnumerable<T> thisEnumerable, IEnumerable<T> otherEnumerable)
        {
            return thisEnumerable.All(otherEnumerable.Contains) && otherEnumerable.All(thisEnumerable.Contains);
        }

        public static int GetRandomIndex<T>(this IEnumerable<T> thisEnumerable)
        {
            return Random.Range(0, thisEnumerable.Count() - 1);
        }

        public static T[] RandomDistinct<T>(this IEnumerable<T> collection, int numDistinct)
        {
            var distinctValues = new HashSet<T>();
            var collectionCopy = new List<T>(collection);

            while ((distinctValues.Count < numDistinct) && (collectionCopy.Count > 0))
            {
                T randomElement = collectionCopy.GetRandomElement();
                collectionCopy.Remove(randomElement);
                distinctValues.Add(randomElement);
            }

            return distinctValues.ToArray();
        }

        public static IEnumerable<Tuple<T, T>> MakeAllPairs<T>(this IEnumerable<T> data)
        {
            List<T> items = data.ToList();

            for (var i = 0; i < items.Count; i++)
            {
                T a = items[i];
                for (int j = i + 1; j < items.Count; j++)
                {
                    T b = items[j];
                    yield return Tuple.Create(a, b);
                }
            }
        }
    }
}
