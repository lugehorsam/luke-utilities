using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    public class View<T> : View {
        
        public event Action<T, T> OnDataChanged
        {
            add { stateMachine.OnStateChanged += value; }
            remove { stateMachine.OnStateChanged -= value; }
        }

        public virtual T Datum {
            set
            {
                stateMachine.State = value;
            }
            get {
                return stateMachine.State;
            }
        }

        private readonly StateMachine<T> stateMachine = new StateMachine<T>();
    
        protected virtual void HandleDatumChanged (T oldData, T newData) {}

        public View()
        {
            stateMachine.OnStateChanged += HandleDatumChanged;
        }

        public View(T datum)
        {
            stateMachine.OnStateChanged += HandleDatumChanged;
            Datum = datum;
        }

        public override string ToString()
        {
            return this.ToString("View", stateMachine.State);
        }       
    }

    public class View
    {
        protected virtual string Name
        {
            get { return "GO"; }
        }
        
        public GameObject GameObject
        {
            get;
        }

        public Transform Transform
        {
            get { return GameObject.GetComponent<Transform>(); }
        }

        public View()
        {
            GameObject = new GameObject();
            GameObject.name = Name;
        }
    }
}
