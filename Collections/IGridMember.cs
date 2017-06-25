namespace Utilities
{
    public interface IGridMember<T> : IGridMember where T : IGridMember<T> {
    
        Grid<T> Grid
        {
            get;
            set;
        }    
    }

    public interface IGridMember {
    
        IGrid Grid
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

        int Index { get; }
    }  
}
