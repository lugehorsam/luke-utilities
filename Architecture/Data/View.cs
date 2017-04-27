using System;
using UnityEngine;

namespace Utilities
{
    public abstract class View<TDatum> : View {
        
        public event Action<TDatum, TDatum> OnDataChanged
        {
            add { stateMachine.OnStateChanged += value; }
            remove { stateMachine.OnStateChanged -= value; }
        }

        public virtual TDatum Datum {
            set
            {
                stateMachine.State = value;
            }
            get {
                return stateMachine.State;
            }
        }

        private readonly StateMachine<TDatum> stateMachine = new StateMachine<TDatum>();
    
        protected virtual void HandleDatumChanged (TDatum oldData, TDatum newData) {}

        public View(TDatum datum)
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
