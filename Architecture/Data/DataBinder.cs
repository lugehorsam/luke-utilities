using UnityEngine;
using System.Collections.Generic;
using Datum;
using System;

public abstract class DataBinder<TDatum, TBehavior> : BehaviorManager<TDatum, TBehavior>
    where TDatum : struct
    where TBehavior : DatumBehavior<TDatum> {

    Action unsubscribeFromSource;
    Action pushToSource;

/**
    public void Push ()
    {
        pushToSource ();
    }

    public void TransferTo (DataBinder<TDatum, TBehavior> receivingBinder,
                               int insertionIndex,
                               TBehavior behavior)
    {
        if (receivingBinder == this) {
            Diagnostics.Report ("Data manager " + this + "Trying to transfer data to itself");
        }

        List<TDatum> silenTDatum = data;
        List<TDatum> receivingSilenTDatum = receivingBinder.data;

        RemoveHandlers (behavior);
        receivingBinder.AddHandlers (behavior);
        silenTDatum.Remove (behavior.DatumPublisher);
        receivingSilenTDatum.Insert (insertionIndex, behavior.DatumPublisher);
        behaviorPool.TransferTo (receivingBinder.behaviorPool, behavior, insertionIndex);
        HandleRemovedBehavior (behavior);
        receivingBinder.HandleNewBehavior (behavior);
        Push ();
        receivingBinder.Push ();
        TryReportBadConfig ();
    }

    public override string ToString ()
    {
        return string.Format ("[DataBinder: Name={0} Data={1}]", gameObject.name, Data.ToFormattedString());
    }

    void TryReportBadConfig ()
    {
        if (data.Count != Behaviors.Count) {
            Diagnostics.Report ("Data and behaviors have uneven counts " + data.ToFormattedString () + " , " + Behaviors.ToFormattedString ());
        }
    }

**/
}
