using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Layout<TData, TBehavior> : DataManager<TData, TBehavior>
    where TData : struct
    where TBehavior : DatumBehavior<TData>
{

    public void DoLayout (int startIndex = 0)
    {
        for (int i = startIndex; i < Behaviors.Count; i++) {
            TBehavior behavior = Behaviors [i];
            behavior.transform.SetParent (transform, worldPositionStays: true);
            behavior.transform.SetSiblingIndex (i);
            ILayoutMember layoutMember = behavior as ILayoutMember;
            if (layoutMember != null)
            {
                layoutMember.OnLocalLayout(GetIdealLocalPosition(behavior));
            }
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

    protected virtual Vector2 GetIdealLocalPosition(TBehavior behavior)
    {
        return default(Vector2);
    }
}