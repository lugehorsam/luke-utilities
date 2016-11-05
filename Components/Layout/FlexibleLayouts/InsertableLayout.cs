using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class InsertableLayout<TData, TBehavior> : FlexibleLayout<TData, TBehavior> 
    where TData : struct
    where TBehavior : DatumBehavior<TData>, 
    IDraggable,
    IMultiCollider2D, 
    ILayoutMember
{
    public event Action<TBehavior> OnBehaviorSelected = (behavior) => { };
    public event Action<TBehavior> OnBehaviorDeselected = (behavior) => { };

    public int? GetInsertionIndex (TBehavior colliderBehavior)
    {
        MultiCollider2D multiCollider = colliderBehavior.GetComponent<MultiCollider2D> ();
        Collider2D leftCollider = multiCollider.GetLeftOverlap ();
        Collider2D rightCollider = multiCollider.GetRightOverlap ();
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
        Diagnostics.Log ("Insertion index for word " + colliderBehavior + " is " + insertionIndex, LogType.Dragging);
        return insertionIndex;
    }

    public bool IsOverlap (TBehavior behavior)
    {
        Collider2D leftOverlap = behavior.MultiCollider.GetLeftOverlap ();
        Collider2D rightOverlap = behavior.MultiCollider.GetRightOverlap ();
        Diagnostics.Log ("Left overlap for behavior " + behavior.gameObject + " is " + leftOverlap, LogType.Dragging);
        return (leftOverlap != null && this.Behaviors.Contains (leftOverlap.GetComponent<TBehavior> ()))
                || (rightOverlap != null && this.Behaviors.Contains (rightOverlap.GetComponent<TBehavior> ()));
    }

    protected override void AddHandlers (TBehavior behavior)
    {
        if (behavior == null) {
            Diagnostics.Report ("Trying to add handlers to a null behavior");
        }
        behavior.Draggable.OnSelect += OnDraggableSelect;
        behavior.Draggable.OnDeselect += OnDraggableDeselect;
    }
     
    protected override void RemoveHandlers (TBehavior behavior)
    {
        if (behavior == null) {
            Diagnostics.Report ("Trying to remove handlers from a null behavior");
        }
        behavior.Draggable.OnSelect -= OnDraggableSelect;
        behavior.Draggable.OnDeselect -= OnDraggableDeselect;
    }

    void OnDraggableSelect (Selectable selectable, Vector3 selectionPos)
    {
        OnBehaviorSelected (selectable.GetComponent<TBehavior> ());
    }

    void OnDraggableDeselect (Selectable selectable)
    {
        OnBehaviorDeselected (selectable.GetComponent<TBehavior> ());
    }
}
