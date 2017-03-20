using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class GridLayoutDatum<T> : ISerializationCallbackReceiver where T : GridMemberDatum<T>, new() {
    
    public T[] GridMemberData
    {
        get { return gridMemberData; }
    }

    private T[] gridMemberData;
    
    [SerializeField]
    private T[] elements;

    public int Rows
    {
        get { return rows; }
    }
    
    [SerializeField]
    private int rows;

    public int Columns
    {
        get { return columns; }
    }

    [SerializeField]
    private int columns;

    public void OnBeforeSerialize()
    {
        
    }
    
    public virtual void OnAfterDeserialize()
    {
        if (!RowsAndColumnsAreDefined())
        {
            throw new Exception("Rows and columns are not defined!");
        }

        if (elements == null)
        {
            Diagnostics.LogWarning("Elements is null");
        }
        
        SetMemberDataFromSerializedElements();
    }

    void SetMemberDataFromSerializedElements()
    {
        int maxIndex = ToIndex(rows, columns);
        gridMemberData = new T[maxIndex + 1];
        for (int i = 0; i <= maxIndex; i++)
        {
            bool serializedContainsIndex = gridMemberData.Any(member => member.Index == i);
            if (!serializedContainsIndex)
            {
                gridMemberData[i] = new T();
                gridMemberData[i].Grid = this;
                gridMemberData[i].Index = i;
            }
        }
    }
       
    public int ToIndex(int row, int col) 
    {
        return row * columns + col;
    }

    bool RowsAndColumnsAreDefined()
    {
        return rows != 0 && columns != 0;
    }    
    
    int[] ToRowCol(int index) {
        return new int[2]{ RowOfIndex(index), ColOfIndex(index)};
    }

    int[] RowColOf(GridMemberDatum<T> startElement) {
        return ToRowCol (Array.IndexOf (gridMemberData, startElement));
    }

    public int RowOfIndex(int index) {
        return (int) Mathf.Floor (index / rows);
    }

    public int ColOfIndex(int index) {
        return index % columns;
    }
}
