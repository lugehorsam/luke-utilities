namespace Utilities
{
    public interface IState
    {
        void HandleTransitionFrom();
        void HandleTransitionTo();
    }
}
