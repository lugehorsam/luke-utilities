using System;
using UnityEngine;

[Serializable]
public class GridMemberDatum : IComparable<GridMemberDatum> {

    public GridLayoutDatum Grid { get; set; }

    public int Index
    {
        get { return Grid.ToIndex(row, column); }
    }

    [SerializeField]
    private int row;

    [SerializeField]
    private int column;

    public int CompareTo(GridMemberDatum otherDatum)
    {
        return Index.CompareTo(otherDatum.Index);
    }
}
