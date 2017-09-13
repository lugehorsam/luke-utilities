namespace Utilities
{
    using System.Collections;
    
    public interface IState
    {
        IEnumerator Exit();
        IEnumerator Enter();
    }
}
