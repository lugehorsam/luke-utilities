namespace Utilities
{
    public interface IState
    {
        void OnExit();
        void OnEnter();
    }
}
