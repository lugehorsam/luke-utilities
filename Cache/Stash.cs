namespace Utilities.Cache
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    [Serializable] public class Stash<T> : IEnumerable<T>, ISerializationCallbackReceiver
    {
        [SerializeField] private T[] array;

        private readonly List<T> _observables = new List<T>();

        public Stash() { }

        public Stash(IEnumerable<T> enumerable)
        {
            foreach (T item in enumerable)
            {
                _observables.Add(item);
            }
        }

        public void OnBeforeSerialize()
        {
            array = _observables.ToArray();
        }

        public void OnAfterDeserialize()
        {
            if (array == null)
            {
                return;
            }

            foreach (T item in array)
            {
                _observables.Add(item);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _observables.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _observables).GetEnumerator();
        }
    }
}
