using System.Collections;
using System.Collections.Generic;
using Utilities;

public interface IGridMember<T> where T : IGridMember<T>, new() {
    
    Grid<T> Grid
    {
        get;
        set;
    }

    int Row
    {
        get;
        set;
    }
    
    int Column
    {
        get;
        set;
    }
    
}
