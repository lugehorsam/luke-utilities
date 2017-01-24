using System;
using System.Collections;
using UnityEngine;

public abstract class DatumBehavior<TDatum> : GameBehavior {

    public event Action<TDatum, TDatum> OnDataChanged = (d1, d2) => { };

    public virtual TDatum Datum {
        set
        {
            Debug.Log("Datum set");
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

    protected virtual IEnumerator Start ()
    {
        yield return null;
    }

    protected virtual void HandleDataUpdate (TDatum oldData, TDatum newData) {}
}
