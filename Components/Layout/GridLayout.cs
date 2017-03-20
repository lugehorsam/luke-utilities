using UnityEngine;
using System;
using Datum;

/// <summary>
/// Arbitrary grid layout. Bototm left is row 0, col 0
/// </summary>
public class GridLayout : Layout {

    private int columns;
    private int rows;

    protected override Vector2 GetIdealLocalPosition(ILayoutMember behavior)
    {
        int behaviorIndex = LayoutMembers.IndexOf(behavior);
        int row = behaviorIndex % columns;
        int col = behaviorIndex / columns;
        return new Vector2(row * 1, col * 1);
    }
}
