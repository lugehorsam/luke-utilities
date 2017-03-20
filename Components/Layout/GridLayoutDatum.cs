using System;
using UnityEngine;

[Serializable]
public class GridLayoutDatum : ISerializationCallbackReceiver {
    
    public ILayoutMember[] GridMemberData
    {
        get { return gridMemberData; }
    }
    
    [SerializeField]
    private ILayoutMember[] gridMemberData;

    [SerializeField]
    private int rows;

    [SerializeField]
    private int columns;

    public void OnBeforeSerialize()
    {
        
    }
    
    public void OnAfterDeserialize()
    {
        if (!RowsAndColumnsAreDefined())
        {
            throw new Exception("Rows and columns are not defined!");
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

    int[] RowColOf(GridMemberDatum startElement) {
        return ToRowCol (Array.IndexOf (gridMemberData, startElement));
    }

    int RowOfIndex(int index) {
        return (int) Mathf.Floor (index / rows);
    }

    int ColOfIndex(int index) {
        return index % columns;
    }
}
