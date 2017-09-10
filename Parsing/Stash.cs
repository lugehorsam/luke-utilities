
namespace Utilities.Serializable
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    [Serializable]
    public class Stash<T> : IEnumerable<T>, ISerializationCallbackReceiver {
    
        [SerializeField] private T[] array;
        
        private readonly Observable.Observables<T> _observables = new Observable.Observables<T>();
        
        public Stash(){}

        public Stash(IEnumerable<T> enumerable)
        {
            foreach (var item in enumerable)
                _observables.Add(item);
        }    

        public void OnBeforeSerialize()
        {
            array = _observables.ToArray();
        }

        public void OnAfterDeserialize()
        {
            Diag.Log("after desrialize");
            if (array == null) return;
            
            foreach (var item in array)
                _observables.Add(item);
        }

        public static implicit operator Observable.Observables<T>(Stash<T> thisStash)
        {
            return thisStash._observables;
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
