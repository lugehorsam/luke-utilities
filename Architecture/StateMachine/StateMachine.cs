using System;
using UnityEngine;

namespace Utilities
{
    [Serializable]
    public class StateMachine<T> : IStateMachine
    {
        public delegate void StateChangedHandler(T oldState, T newState);
        
        public event StateChangedHandler OnStateChanged = (arg1, arg2) => { };

        public T State
        {
            get { return _state; }
            set
            {
                if (value != null && value.Equals(_state))
                {
                    return;
                }

                T oldProperty = _state;
                _state = value;

                HandleStateTransition(oldProperty, _state);

                IState oldState = oldProperty as IState;
                IState newState = value as IState;
                       
                if (oldState != null)
                    oldState.OnExit();
        
                if (newState != null)
                    newState.OnEnter();

                OnStateChanged(oldProperty, value);
            }
        }

        [SerializeField]
        private T _state;

        public StateMachine(T initialState)
        {
            State = initialState;
        }

        protected virtual void HandleStateTransition(T oldState, T newState)
        {
        }
        
        public StateMachine() {}
        
        public static implicit operator T(StateMachine<T> machine)
        {
            return machine.State;
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