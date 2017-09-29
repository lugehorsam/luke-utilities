namespace Utilities
{
    using System.Collections;
    
    public interface IState
    {
        IEnumerator Run();
        IState NextState { get; }
    }
}
