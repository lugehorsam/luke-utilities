namespace Utilities.Command
{
    public class SerialCommand : Command
    {
        private int _currentIndex;
        
        public override object Current { get; }

        protected override bool MoveNextInternal()
        {
            if (_currentIndex >= _commandSteps.Count)
            {
                return false;
            }

            if (!_commandSteps[_currentIndex].MoveNext())
            {
                _currentIndex++;
            }

            return _currentIndex < _commandSteps.Count;
        }

        public override void Reset() { }

        protected override void CancelInternal()
        {
            _commandSteps[_currentIndex].Cancel();
        }
    }
}
