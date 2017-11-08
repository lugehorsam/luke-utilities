using System.Linq;
using System.Collections.Generic;

namespace Utilities
{ 
    public static class IEnumerableExtensions {

        public static string Pretty<T>(this IEnumerable<T> thisEnumerable) {
            
            if (thisEnumerable == null) 
            {
                return "";
            }

            string formattedString = "";
            
            foreach (T item in thisEnumerable) 
            {
                formattedString += item.ToString();

                if (!item.Equals(thisEnumerable.LastOrDefault()))
                {
                    string formatter = ", ";

                    if (item is KeyValuePair<object, object>)
                    {
                        formatter = ": ";
                    }
                    
                    formattedString += formatter;
                }
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

        public static List<T> ToGenericBase<TGeneric, T>(TGeneric genericEnumerable) where TGeneric : IEnumerable<T>
        {
            var baseEnumerable = new List<T>();
            foreach (var item in genericEnumerable)
            {
                baseEnumerable.Add(item);
            }
            return baseEnumerable;
        }

        //Order doesn't matter
        public static bool IdenticalContent<T>(this IEnumerable<T> thisEnumerable, IEnumerable<T> otherEnumerable)
        {
            return thisEnumerable.All(otherEnumerable.Contains) && otherEnumerable.All(thisEnumerable.Contains);
        }
        
        public static int GetRandomIndex<T>(this IEnumerable<T> thisEnumerable)
        {
            return Randomizer.Randomize(0, thisEnumerable.Count() - 1);
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
    }  
}
