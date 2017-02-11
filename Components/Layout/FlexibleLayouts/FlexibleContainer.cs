using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

/**
[Serializable]
public abstract class FlexibleContainer<TTargeTDatum, TBehavior> : Container<TTargeTDatum, TBehavior>
    where TTargeTDatum : struct
    where TBehavior : DatumBehavior<TTargeTDatum>, ILayoutMember
{ 
    [SerializeField]
    bool wrap;
    [SerializeField]
    float wrapThreshold;
    [SerializeField]
    FlexibleSpacingPolicy spacingPolicy;
    [SerializeField]
    FlexibleFlowPolicy flowPolicy;
    [SerializeField]
    float xSpacing;
    [SerializeField]
    float ySpacing;

    //TODO Optimize rec calls with dynamic programming
    protected override Vector2 GetIdealLocalPosition(TBehavior behavior) {
        Vector2 newPosition = Vector2.zero;
        int currentBehaviorIndex = Behaviors.IndexOf(behavior);

        if (currentBehaviorIndex <= 0) {
            return newPosition;
        }

        TBehavior previousBehavior = Behaviors[currentBehaviorIndex - 1];

        //recursive
        Vector2 previousBehaviorPosition = GetIdealLocalPosition (previousBehavior);
        newPosition = previousBehaviorPosition;
        newPosition += GetSpacingVector (previousBehavior);
        newPosition += GetPaddingVector ();
        newPosition = ClampByFlow (previousBehaviorPosition, newPosition);
        if (ShouldWrap(behavior, newPosition)) {
            newPosition = GetLocalPositionFromWrap (newPosition);
        }
        return newPosition;
    }

    Vector2 GetLocalPositionFromWrap(Vector2 newPosition) {
        //TODO implement for vertical
        switch (flowPolicy) {
        case FlexibleFlowPolicy.Horizontal:
        default:            
            return new Vector2 (0, newPosition.y - ySpacing);
        }
    }

    bool ShouldWrap(TBehavior newItem, Vector2 newPosition) {
        //TODO implement for vertical
        return wrap && newPosition.x > wrapThreshold;
    }

    Vector2 GetSpacingVector(TBehavior previousBehavior) {
        if (spacingPolicy == FlexibleSpacingPolicy.Collider2D) {
            Vector2 size = previousBehavior.GetComponent<Collider2D> ().bounds.size;
            return size;
        } else if (spacingPolicy == FlexibleSpacingPolicy.Renderer) {
            Renderer renderer = previousBehavior.GetComponent<Renderer> ();
            Bounds bounds = renderer.bounds;
        }
        return Vector2.zero;
    }

    Vector2 GetPaddingVector() {
        return new Vector2 (xSpacing, ySpacing);
    }

    Vector2 ClampByFlow(Vector2 lastItemPos, Vector2 newGameObjPos) {
        switch (flowPolicy) {
        case FlexibleFlowPolicy.None:
            return newGameObjPos;
        case FlexibleFlowPolicy.Horizontal:
            return new Vector2 (newGameObjPos.x, lastItemPos.y);
        case FlexibleFlowPolicy.Vertical:
            return new Vector2 (lastItemPos.x, newGameObjPos.y);
        default:
            return Vector2.zero;
        }
    }
}
**/