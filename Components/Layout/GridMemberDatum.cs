using System;
using UnityEngine;

[Serializable]
public class GridMemberDatum<T> : IComparable<GridMemberDatum<T>> where T : GridMemberDatum<T>, new()
{
    public GridLayoutDatum<T> Grid { get; set; }

    public int Index
    {
        get { return Grid.ToIndex(row, column); }
        set
        {
            row = Grid.RowOfIndex(value);
            column = Grid.ColOfIndex(value);
        }
    }
    
    [SerializeField]
    private int row;

    [SerializeField]
    private int column;

    public int CompareTo(GridMemberDatum<T> otherDatum)
    {
        return Index.CompareTo(otherDatum.Index);
    }
}
