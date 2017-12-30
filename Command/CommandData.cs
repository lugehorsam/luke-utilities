namespace Utilities.Command
{
    using System;
    using System.Collections;

    /// <summary>
    ///     A private wrapper on <see cref="IEnumerator" /> that consolidates serial commands, parallel commands, and
    ///     actions.
    /// </summary>
    internal class CommandData : IEnumerator
    {
        private readonly Func<IEnumerator> _enumeratorFunc;

        private IEnumerator _enumerator;

        private readonly Action _action;

        public object Current => _enumerator?.Current;

        public CommandMode CommandMode { get; }

        public CommandData(IEnumerator enumerator, CommandMode commandMode)
        {
            _enumerator = enumerator;
            CommandMode = commandMode;
        }

        public CommandData(Action action, CommandMode commandMode)
        {
            _action = action;
            CommandMode = commandMode;
        }

        public CommandData(Func<IEnumerator> enumeratorFunc, CommandMode commandMode)
        {
            _enumeratorFunc = enumeratorFunc;
            CommandMode = commandMode;
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
            _enumerator.Reset();
        }
    }
}
