namespace Utilities.Command
{
    using System;
    using System.Collections;

    public class CommandStep : IEnumerator
    {
        private readonly Action _action;
        private readonly Func<IEnumerator> _enumeratorFunc;

        private IEnumerator _enumerator;

        public CommandStep(Func<IEnumerator> enumeratorFunc)
        {
            _enumeratorFunc = enumeratorFunc;
        }

        public CommandStep(IEnumerator enumerator)
        {
            _enumerator = enumerator;
        }

        public CommandStep(Action action)
        {
            _action = action;
        }

        public static implicit operator CommandStep(Action thisAction)
        {
            return new CommandStep(thisAction);
        }
        
        public static implicit operator CommandStep(Func<IEnumerator> thisAction)
        {
            return new CommandStep(thisAction);            
        }
        
        public bool MoveNext()
        {
            if (_action != null)
            {
                _action();
                return false;
            }

            if ((_enumerator == null) && (_enumeratorFunc != null))
            {
                _enumerator = _enumeratorFunc();
            }

            return (_enumerator != null) && _enumerator.MoveNext();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public object Current { get; }

        public void Cancel()
        {
            (_enumerator as Command)?.Cancel();
        }
    }
}
