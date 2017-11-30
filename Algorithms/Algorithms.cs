namespace Utilities.Algorithms
{
    using UnityEngine;

    public static class Shuffle
    {
        public static T[] DoShuffle<T>(T[] collection)
        {
            int index = collection.Length - 1;
            T[] newCollection = collection;

            while (index > 0)
            {
                var newIndex = (int) Mathf.Floor(Random.value * collection.Length);
                T unplacedValue = newCollection[newIndex];
                newCollection[newIndex] = newCollection[index];
                newCollection[index] = unplacedValue;
                index--;
            }

            return newCollection;
        }
    }
}
