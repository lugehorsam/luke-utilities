using System.Linq;
using System.Collections.Generic;

namespace Utilities
{ 
    public static class IEnumerableExtensions {

        public static string ToFormattedString<T>(this IEnumerable<T> thisEnumerable) {
            
            if (thisEnumerable == null) 
            {
                return "";
            }

            string formattedString = "";
            
            foreach (T item in thisEnumerable) 
            {
                formattedString += item.ToString();

                if (!item.Equals(thisEnumerable.LastOrDefault()))
                    formattedString += ", ";
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
    }  
}
