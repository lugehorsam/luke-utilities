using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    public class View<T> : View {
        
        public event StateMachine<T>.StateChangedHandler OnDataChanged
        {
            add { stateMachine.OnStateChanged += value; }
            remove { stateMachine.OnStateChanged -= value; }
        }
       
         protected T Data {
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

        public View(T data)
        {
            stateMachine.OnStateChanged += HandleDatumChanged;
            Data = data;
        }

        public bool HasData(T datum)
        {
            return Data.Equals(datum);
        }

        public void SetData(T datum)
        {
            Data = datum;
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
