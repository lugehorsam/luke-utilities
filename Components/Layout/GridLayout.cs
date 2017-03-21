using UnityEngine;

/// <summary>
/// Arbitrary grid layout. Bottom left is row 0, col 0
/// </summary>
public class GridLayout : Layout {

    private readonly int columns;
    private readonly int rows;

    public GridLayout(int rows, int columns)
    {
        this.rows = rows;
        this.columns = columns;
    }

    protected override Vector2 GetIdealLocalPosition(ILayoutMember behavior)
    {
        int behaviorIndex = LayoutMembers.IndexOf(behavior);
        int row = behaviorIndex % columns;
        int col = behaviorIndex / columns;
        return new Vector2(row * 1, col * 1);
    }
}
