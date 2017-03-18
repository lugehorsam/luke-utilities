using UnityEngine;
using System;


/// <summary>
/// Arbitrary grid layout. Bototm left is row 0, col 0
/// </summary>
public abstract class GridLayout<TDatum, TBehavior> : Layout<TDatum, TBehavior>
    where TDatum : struct
    where TBehavior : DatumBehavior<TDatum>, ILayoutMember {

    [SerializeField]
    int rows;

    [SerializeField]
    int cols;

    [SerializeField]
    float rowSpacing;

    [SerializeField]
    float colSpacing;

    public GridLayout(Func<TBehavior> factory) : base(factory)
    {
    }

    protected override Vector2 GetIdealLocalPosition(TBehavior behavior)
    {
        int behaviorIndex = Behaviors.IndexOf(behavior);
        int row = behaviorIndex % cols;
        int col = behaviorIndex / cols;
        return new Vector2(row * rowSpacing, col * colSpacing);
    }
}
