using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class DatumBehavior<TDatum> : GameBehavior
    where TDatum : struct {

    public event Action<TDatum, TDatum> OnDataChanged = (d1, d2) => { };

    public virtual TDatum Datum {
        set {
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
