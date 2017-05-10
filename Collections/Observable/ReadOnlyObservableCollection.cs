using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Utilities
{
    public class ReadOnlyObservableCollection<T> : IObservableCollection<T>
    {
        public event Action<T> OnAfterItemRemove
        {
            add { _observableCollection.OnAfterItemRemove += value; }
            remove
            {
                _observableCollection.OnAfterItemRemove -= value;
            }
        }

        public ReadOnlyCollection<T> Items { get { return new ReadOnlyCollection<T>(_observableCollection); } }

        public event Action<T> OnAfterItemAdd
        {
            add
            {
                _observableCollection.OnAfterItemAdd += value;
            }
            remove
            {
                _observableCollection.OnAfterItemAdd -= value;
            }
        }

        protected readonly ObservableCollection<T> _observableCollection;

        public ReadOnlyObservableCollection(ObservableCollection<T> collection)
        {
            _observableCollection = collection;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _observableCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _observableCollection).GetEnumerator();
        }

        void ICollection<T>.Add(T item)
        {
            throw new NotSupportedException();
        }

        void ICollection<T>.Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(T item)
        {
            return _observableCollection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _observableCollection.CopyTo(array, arrayIndex);
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new NotSupportedException();
        }

        public int Count
        {
            get { return _observableCollection.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }
    }
}
