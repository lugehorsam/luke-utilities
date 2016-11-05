using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class Layout<TData, TBehavior> : DataManager<TData, TBehavior>
    where TData : struct
    where TBehavior : DatumBehavior<TData>, ILayoutMember
{

    public void DoLayout (int startIndex = 0)
    {
        for (int i = startIndex; i < Behaviors.Count; i++) {
            TBehavior behavior = Behaviors [i];
            Diagnostics.Log ("Laying out behavior " + behavior, LogType.Layouts);
            behavior.transform.SetParent (transform, worldPositionStays: true);
            behavior.transform.SetSiblingIndex (i);
            behavior.OnLocalLayout (GetIdealLocalPosition (behavior));
        }
    }

    protected override void HandleNewBehavior (TBehavior behavior)
    {
        DoLayout ();
    }

    protected override void HandleRemovedBehavior (TBehavior behavior)
    {
        DoLayout ();
    }

    protected abstract Vector2 GetIdealLocalPosition (TBehavior behavior);
}