namespace Utilities.Cache
{
    using System;
    using System.Collections.Generic;

    using UnityEngine;

    [Serializable] public class SerializableList<T> : List<T>, ISerializationCallbackReceiver
    {
        [SerializeField] private T[] _array;

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
            _array = ToArray();
        }

        public void OnAfterDeserialize()
        {
            if (_array == null)
            {
                return;
            }

            foreach (T item in _array)
            {
                Add(item);
            }
        }
    }
}
