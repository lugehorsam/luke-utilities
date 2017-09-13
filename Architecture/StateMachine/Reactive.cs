using System;
using UnityEngine;

namespace Utilities
{
    [Serializable]
    public class Reactive<T>
    {
        public delegate void PropertyChangedHandler(T oldState, T newState);
        
        public event PropertyChangedHandler OnPropertyChanged = (arg1, arg2) => { };

        public T Value
        {
            get { return _value; }
            set
            {
                if (value != null && value.Equals(_value))
                {
                    return;
                }

                T oldProperty = _value;
                _value = value;

                HandleStateTransition(oldProperty, _value);

                OnPropertyChanged(oldProperty, value);
            }
        }

        [SerializeField]
        private T _value;

        public Reactive(T initialValue)
        {
            Value = initialValue;
        }

        protected virtual void HandleStateTransition(T oldState, T newState)
        {
        }
        
        public Reactive() {}
        
        public static implicit operator T(Reactive<T> machine)
        {
            return machine.Value;
        }
        
        public static bool IsChange<K>(T data1, T data2, Func<T, K> getProperty, out K property1, out K property2)
        {
            property1 = default(K);
            property2 = default(K);
            
            if (data1 == null && data2 == null)
            {
                return false;
            }
            
            if (data1 == null)
            {
                property2 = getProperty(data2);
                return !property2.Equals(default(K));
            }

            if (data2 == null)
            {
                property1 = getProperty(data1);
                return !property1.Equals(default(K));
            }

            return !getProperty(data1).Equals(data2);
        }
    }   
}