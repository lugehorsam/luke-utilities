using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class InsertableLayout<TData, TBehavior> : FlexibleLayout<TData, TBehavior> 
    where TData : struct
    where TBehavior : DatumBehavior<TData>, 
    IMultiCollider2D,
    ITouchDispatcher,
    ILayoutMember
{
    public event Action<TBehavior> OnBehaviorSelected = (behavior) => { };
    public event Action<TBehavior> OnBehaviorDeselected = (behavior) => { };

    public int? GetInsertionIndex (TBehavior colliderBehavior)
    {
        MultiCollider multiCollider = colliderBehavior.GetComponent<MultiCollider> ();
        Collider leftCollider = multiCollider.GetLeftOverlap ();
        Collider rightCollider = multiCollider.GetRightOverlap ();
        int insertionIndex;

        if (leftCollider == null && rightCollider == null) {
            return null;
        } else {
            List<TData> dataToIgnore = new List<TData> ();
            if (Data.Contains (colliderBehavior.Datum)) {
                dataToIgnore.Add (colliderBehavior.Datum);
            }
            if (leftCollider != null) {
                TBehavior leftBehavior = leftCollider.GetComponent<TBehavior> ();
                insertionIndex = Data.IndexOf (leftBehavior.Datum) + 1;
            } else { 
                //right collider not null
                TBehavior rightBehavior = rightCollider.GetComponent<TBehavior> ();
                insertionIndex = Data.IndexOf (rightBehavior.Datum);
            }
        }

        return insertionIndex;
    }

    public bool IsOverlap (TBehavior behavior)
    {
        Collider leftOverlap = behavior.MultiCollider.GetLeftOverlap ();
        Collider rightOverlap = behavior.MultiCollider.GetRightOverlap ();

        return (leftOverlap != null && this.Behaviors.Contains (leftOverlap.GetComponent<TBehavior> ()))
                || (rightOverlap != null && this.Behaviors.Contains (rightOverlap.GetComponent<TBehavior> ()));
    }

    protected override void AddHandlers (TBehavior behavior)
    {
        if (behavior == null) {
            Diagnostics.Report ("Trying to add handlers to a null behavior");
        }
        behavior.TouchDispatcher.OnTouch += OnDraggableSelect;
        behavior.TouchDispatcher.OnRelease += OnDraggableDeselect;
    }
     
    protected override void RemoveHandlers (TBehavior behavior)
    {
        if (behavior == null) {
            Diagnostics.Report ("Trying to remove handlers from a null behavior");
        }
        behavior.TouchDispatcher.OnTouch -= OnDraggableSelect;
        behavior.TouchDispatcher.OnRelease -= OnDraggableDeselect;
    }

    void OnDraggableSelect (TouchDispatcher selectable, Gesture gesture)
    {
        OnBehaviorSelected (selectable.GetComponent<TBehavior> ());
    }

    void OnDraggableDeselect (TouchDispatcher selectable, Gesture gesture)
    {
        OnBehaviorDeselected (selectable.GetComponent<TBehavior> ());
    }
}
