using System;
using UnityEngine;

namespace Utilities
{
    public abstract class View<TDatum> : View {
        
        public event Action<TDatum, TDatum> OnDataChanged
        {
            add { datum.OnStateChanged += value; }
            remove { datum.OnStateChanged -= value; }
        }

        public virtual TDatum Datum {
            set
            {
                datum.State = value;
            }
            get {
                return datum;
            }
        }

        private readonly StateMachine<TDatum> datum = new StateMachine<TDatum>();
    
        protected virtual void HandleDatumChanged (TDatum oldData, TDatum newData) {}

        public View(TDatum datum)
        {
            Datum = datum;
        }

        public override string ToString()
        {
            return this.ToString(datum);
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
