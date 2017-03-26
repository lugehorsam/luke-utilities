﻿using System;
using UnityEngine;
using Utilities;

[Serializable]
public class GridMember<T> : IGridMember<T>, IComparable<GridMember<T>> where T : GridMember<T>, new()
{
    public Grid<T> Grid { get; set; }

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

    public int CompareTo(GridMember<T> other)
    {
        return Index.CompareTo(other.Index);
    }
}