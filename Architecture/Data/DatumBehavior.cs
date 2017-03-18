using System;
using System.Collections;
using UnityEngine;

public class DatumBehavior<TDatum> {

    public event Action<TDatum, TDatum> OnDataChanged = (d1, d2) => { };

    public virtual TDatum Datum {
        set
        {
            TDatum oldData = datum;
            datum = value;
            HandleDataUpdate (oldData, datum);
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
    
    protected virtual void HandleDataUpdate (TDatum oldData, TDatum newData) {}
}
