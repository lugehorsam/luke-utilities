namespace Utilities.Cache
{
    using System;
    using System.Collections.Generic;

    using UnityEngine;

    [Serializable] public class SerializableList<T> : List<T>, ISerializationCallbackReceiver
    {
        [SerializeField] private T[] array;

        public SerializableList() { }

        public SerializableList(IEnumerable<T> enumerable)
        {
            foreach (T item in enumerable)
            {
                Add(item);
            }
        }

        public void OnBeforeSerialize()
        {
            array = ToArray();
        }

        public void OnAfterDeserialize()
        {
            if (array == null)
            {
                return;
            }

            foreach (T item in array)
            {
                Add(item);
            }
        }
    }
}
