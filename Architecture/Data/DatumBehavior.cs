using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class DatumBehavior<TData> : MonoBehaviour
    where TData : struct {

    public event Action<TData, TData> OnDataChanged = (d1, d2) => { };

    public virtual TData Datum {
        set {
            TData oldData = datum;
            datum = value;
            HandleDataUpdate (oldData, datum);
            OnDataChanged (oldData, datum);
        }
        get {
            return datum;
        }
    }

    TData datum;

    protected virtual IEnumerator Start ()
    {
        yield return null;
    }

    public abstract void Init ();

    protected virtual void HandleDataUpdate (TData oldData, TData newData) {}
}
