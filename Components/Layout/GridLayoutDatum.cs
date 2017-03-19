using System;
using UnityEngine;

[Serializable]
public class GridLayoutDatum : ISerializationCallbackReceiver
{
    [SerializeField]
    private GridMemberDatum[] gridMemberData;

    [SerializeField]
    private int rows;

    [SerializeField]
    private int cols;

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
        return row * cols + col;
    }

    bool RowsAndColumnsAreDefined()
    {
        return rows != 0 && cols != 0;
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
        return index % cols;
    }
}
