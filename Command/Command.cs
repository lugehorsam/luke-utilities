namespace Utilities.Command
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    ///     A private wrapper on <see cref="IEnumerator" /> that consolidates serial commands, parallel commands, and
    ///     actions.
    /// </summary>
    public abstract class Command : IEnumerator
    {    
        public delegate void CommandCancelHandler(Command command);

        public event CommandCancelHandler OnCancel = delegate { };

        private bool _cancelled;

        public abstract object Current { get; }

        protected readonly List<CommandStep> _commandSteps = new List<CommandStep>();
        
        public void Add(IEnumerator enumerator)
        {
            _commandSteps.Add(new CommandStep(enumerator));
        }

        public void Add(Action action)
        {
            _commandSteps.Add(new CommandStep(action));
        }
                
        public void Add(Func<IEnumerator> enumeratorFunc)
        {
            _commandSteps.Add(new CommandStep(enumeratorFunc));
        }

        public void Cancel()
        {
            _cancelled = true;
            OnCancel(this);
        }

        public abstract void Reset();

        public bool MoveNext()
        {
            if (_cancelled)
            {
                return false;
            }

            return MoveNextInternal();
        }
        
        protected abstract bool MoveNextInternal();
        
    }
}
