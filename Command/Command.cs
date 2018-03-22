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

        protected readonly List<CommandStep> _commandSteps = new List<CommandStep>();

        private bool _cancelled;

        public abstract object Current { get; }

        public abstract void Reset();

        public bool MoveNext()
        {
            return !_cancelled && MoveNextInternal();
        }

        public event CommandCancelHandler OnCancel = delegate { };

        public static T Create<T>(params CommandStep[] commandSteps) where T : Command, new()
        {
            var command = new T();
            
            foreach (var commandStep in commandSteps)
            {
                command.Add(commandStep);    
            }

            return command;
        }

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
            CancelInternal();
            OnCancel(this);
        }

        protected abstract bool MoveNextInternal();

        protected abstract void CancelInternal();
    }
}
