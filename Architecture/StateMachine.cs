using System;
using UnityEngine;

namespace Utilities
{
    [Serializable]
    public class StateMachine<T>
    {
        public delegate void StateChangedHandler(T oldState, T newState);
        
        public event StateChangedHandler OnStateChanged = (arg1, arg2) => { };

        public T State
        {
            get { return _state; }
            set
            {
                if (value.Equals(_state))
                {
                    return;
                }

                T oldProperty = _state;
                _state = value;

                IState oldState = oldProperty as IState;
                IState newState = value as IState;
        
                if (oldState != null)
                    oldState.HandleTransitionFrom();
        
                if (newState != null)
                    newState.HandleTransitionTo();

                OnStateChanged(oldProperty, value);
            }
        }

        [SerializeField]
        private T _state;

        public StateMachine(T initialState)
        {
            State = initialState;
        }
    
        public StateMachine() {}    
    }   
}