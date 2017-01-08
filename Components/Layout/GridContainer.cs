using UnityEngine;
using System.Collections;

/// <summary>
/// Arbitrary grid layout. Bototm left is row 0, col 0
/// </summary>
public abstract class GridContainer<TData, TBehavior> : Container<TData, TBehavior>
    where TData : struct
    where TBehavior : DatumBehavior<TData>, ILayoutMember {

    [SerializeField]
    int rows;

    [SerializeField]
    int cols;

    [SerializeField]
    float rowSpacing;

    [SerializeField]
    float colSpacing;

    protected override Vector2 GetIdealLocalPosition(TBehavior behavior)
    {
        int behaviorIndex = Behaviors.IndexOf(behavior);
        int row = behaviorIndex % cols;
        int col = behaviorIndex / cols;
        return new Vector2(row * rowSpacing, col * colSpacing);
    }
}
