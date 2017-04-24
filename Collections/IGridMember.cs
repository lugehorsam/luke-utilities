using System.Collections;
using System.Collections.Generic;
using Utilities;

public interface IGridMember<T> where T : IGridMember<T> {
    
    Grid<T> Grid
    {
        get;
        set;
    }    
    
    int Row
    {
        get;
    }
    
    int Column
    {
        get;
    }
}
