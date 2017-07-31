using UnityEngine;
using Utilities;

public class FlexibleLayout<T> : Layout<T, ObservableCollection<T>> where T : ILayoutMember
{
    public bool Wrap { get; set; }
    
    public float WrapThreshold { get; set; }

    public FlexibleSpacingPolicy SpacingPolicy { get; set; }

    public FlexibleFlowPolicy FlowPolicy { get; set; }

    public float XSpacing { get; set; }
    public float YSpacing { get; set; }

    public FlexibleLayout() : base(new ObservableCollection<T>())
    {
        
    }
    
    //TODO Optimize rec calls with dynamic programming
    protected override Vector2 GetIdealLocalPosition(T behavior) {
        Vector2 newPosition = Vector2.zero;
        
        int currentBehaviorIndex = Data.IndexOf(behavior);

        if (currentBehaviorIndex <= 0) {
            return newPosition;
        }

        T previousBehavior = Data[currentBehaviorIndex - 1];

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
        switch (FlowPolicy) {
        case FlexibleFlowPolicy.Horizontal:
        default:            
            return new Vector2 (0, newPosition.y - YSpacing);
        }
    }

    bool ShouldWrap(T newItem, Vector2 newPosition) {
        //TODO implement for vertical
        return Wrap && newPosition.x > WrapThreshold;
    }

    Vector2 GetSpacingVector(T previousBehavior) 
    {
        if (SpacingPolicy == FlexibleSpacingPolicy.Collider2D) {
            Vector2 size = previousBehavior.GameObject.GetComponent<Collider2D> ().bounds.size;
            return size;
        } 
        
        if (SpacingPolicy == FlexibleSpacingPolicy.Collider) {
            Vector2 size = previousBehavior.GameObject.GetComponent<Collider> ().bounds.size;
            return size;
        }
        
        if (SpacingPolicy == FlexibleSpacingPolicy.Renderer) {
            Renderer renderer = previousBehavior.GameObject.GetComponent<Renderer> ();
            Bounds bounds = renderer.bounds;
        }
        return Vector2.zero;
    }

    Vector2 GetPaddingVector() {
        return new Vector2 (XSpacing, YSpacing);
    }

    Vector2 ClampByFlow(Vector2 lastItemPos, Vector2 newGameObjPos) {
        switch (FlowPolicy) {
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
