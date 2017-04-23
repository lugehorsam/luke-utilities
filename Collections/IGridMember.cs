using System.Collections;
using System.Collections.Generic;
using Utilities;

public interface IGridMember<T> : IGridMember where T : IGridMember<T> {
    
    Grid<T> Grid
    {
        get;
        set;
    }    
}

public interface IGridMember
{
    int Row
    {
        get;
    }
    
    int Column
    {
        get;
    }
}