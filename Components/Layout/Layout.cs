using UnityEngine;
using Datum;
using System;

public class Layout<TDatum, TBehavior> : BehaviorManager<TDatum, TBehavior>
    where TDatum : GridMemberDatum
    where TBehavior : DatumBehavior<TDatum> {

    [SerializeField] private int siblingOffset;

    public Layout(Func<TBehavior> factory) : base(factory)
    {
    }

    public void DoLayout (int startIndex = 0)
    {
        for (int i = startIndex; i < Behaviors.Count; i++) {
            TBehavior behavior = Behaviors [i];
            behavior.GameObject.transform.SetSiblingIndex (i + siblingOffset);
            ILayoutMember layoutMember = behavior as ILayoutMember;
            if (layoutMember != null)
            {
                layoutMember.OnLocalLayout(GetIdealLocalPosition(behavior));
            }
        }
    }

    protected sealed override void HandleNewBehavior (TBehavior behavior)
    {
        HandleNewBehaviorPreLayout(behavior);
        DoLayout ();
    }

    protected sealed override void HandleRemovedBehavior (TBehavior behavior)
    {
        HandleRemovedBehaviorPreLayout(behavior);
        DoLayout ();
    }

    protected virtual void HandleNewBehaviorPreLayout(TBehavior behavior) {}

    protected virtual void HandleRemovedBehaviorPreLayout(TBehavior behavior) {}

    protected virtual Vector2 GetIdealLocalPosition(TBehavior behavior)
    {
        return default(Vector2);
    }
}
