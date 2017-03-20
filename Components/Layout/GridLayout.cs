using UnityEngine;

/// <summary>
/// Arbitrary grid layout. Bototm left is row 0, col 0
/// </summary>
public class GridLayout<T> : Layout where T : GridMemberDatum {

    private int columns;
    private int rows;

    public GridLayout(GridLayoutDatum<T> layoutDatum)
    {
        columns = layoutDatum.Columns;
        rows = layoutDatum.Rows;
    }

    protected override Vector2 GetIdealLocalPosition(ILayoutMember behavior)
    {
        int behaviorIndex = LayoutMembers.IndexOf(behavior);
        int row = behaviorIndex % columns;
        int col = behaviorIndex / columns;
        return new Vector2(row * 1, col * 1);
    }
}
