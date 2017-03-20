using System;
using UnityEngine;

namespace Datum
{
    public class DatumBehavior<TDatum> : IGameObject {

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

        public GameObject GameObject
        {
            get;
            private set;
        }

        public DatumBehavior()
        {
            GameObject = new GameObject();
        }
    
        protected virtual void HandleAfterDatumUpdate (TDatum oldData, TDatum newData) {}
    }
}