namespace Utilities.Observable
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    
    public class ReadOnlyObservableCollection<T> : IObservableCollection<T>
    {
        public event Action<T> OnAfterItemRemove
        {
            add { Observables.OnAfterItemRemove += value; }
            remove
            {
                Observables.OnAfterItemRemove -= value;
            }
        }

        public ReadOnlyCollection<T> Items => new ReadOnlyCollection<T>(Observables);

        public event Action<T> OnAfterItemAdd
        {
            add
            {
                Observables.OnAfterItemAdd += value;
            }
            remove
            {
                Observables.OnAfterItemAdd -= value;
            }
        }

        protected readonly Observables<T> Observables;

        public ReadOnlyObservableCollection(Observables<T> observables)
        {
            Observables = observables;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Observables.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Observables).GetEnumerator();
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
            return Observables.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Observables.CopyTo(array, arrayIndex);
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new NotSupportedException();
        }

        public int Count => Observables.Count;

        public bool IsReadOnly => true;
    }
}
