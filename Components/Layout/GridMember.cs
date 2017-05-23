using System;
using UnityEngine;
using Utilities;

[Serializable]
public class GridMember : IGridMember, IComparable<GridMember>
{
    public IGrid Grid { get; set; }

    public int Index
    {
        get { return Grid.ToIndex(row, column); }
    }

    public int Row
    {
        get { return row; }
        set { row = value; }
    }
    
    [SerializeField]
    private int row;

    public int Column
    {
        get { return column; }
        set { column = value; }
    }
    
    [SerializeField]
    private int column;

    public int CompareTo(GridMember other)
    {
        return Index.CompareTo(other.Index);
    }

    public override string ToString()
    {
        return this.ToString(Row, Column);
    }

    public GridMember(int row, int column)
    {
        this.row = row;
        this.column = column;
    }

    public GridMember()
    {
    }
}
