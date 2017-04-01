using System;
using UnityEngine;

namespace Utilities
{
    public abstract class View<TDatum> {
        
        public GameObject GameObject
        {
            get;
        }

        public Transform Transform
        {
            get { return GameObject.GetComponent<Transform>(); }
        }

        public event Action<TDatum, TDatum> OnDataChanged = (d1, d2) => { };

        public virtual TDatum Datum {
            set
            {
                TDatum oldData = datum;
                datum = value;
                HandleAfterDatumUpdate (oldData, datum);
                OnDataChanged (oldData, datum);
            }
            get {
                return datum;
            }
        }

        TDatum datum;
    
        protected virtual void HandleAfterDatumUpdate (TDatum oldData, TDatum newData) {}

        public View(TDatum datum)
        {
            GameObject = new GameObject();
            Datum = datum;
        }
    }   
}
