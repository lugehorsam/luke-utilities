namespace Utilities.Command
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     Holds a queue of enumerators and executes them either in serial or parallel.
    /// </summary>
    public class Command : IEnumerator
    {
        public string Id { get; set; }
        
        public event CommandCompleteHandler OnCommandComplete = delegate { };

        public delegate void CommandCompleteHandler(Command command);

        private bool _lastMoveNextResult = false;
        
        /// <summary>
        ///     A list of all serial and parallel commands in the queue. Commands are never removed from this list.
        ///     Instead the <see cref="_currCommandIndex" /> increments as it runs commands in sequence.
        /// </summary>
        private readonly List<CommandData> _commands = new List<CommandData>();

        /// <summary>
        ///     All active parallel commands placed from <see cref="_commands" /> that have not yet completed.
        /// </summary>
        private readonly List<CommandData> _activeParallelCommands = new List<CommandData>();

        /// <summary>
        ///     The index of the last run command from <see cref="_commands" />.
        /// </summary>
        private int _currCommandIndex;

        /// <summary>
        ///     Whether or not this command queue should reset itself.
        /// </summary>
        private bool _shouldReset;

        /// <summary>
        ///     Unity's Coroutine system handles this property in a special manner. It's best not to deal with this and
        ///     just return null instead.
        ///     See details in https://docs.unity3d.com/ScriptReference/CustomYieldInstruction.html
        /// </summary>
        public object Current => null;

        /// <summary>
        ///     Calls <see cref="MoveNext" /> on the active parallel commands and the active serial command. Only returns
        ///     false if there are no serial and no parallel enumerators left, or if the queue has been <see cref="Reset" />.
        /// </summary>
        public bool MoveNext()
        {
            if (DoReset())
            {
                return _lastMoveNextResult = false;
            }

            bool anyQueuedCommands = _currCommandIndex < _commands.Count;
            bool anyActiveParallelCommands = _activeParallelCommands.Any();

            if (!anyQueuedCommands && !anyActiveParallelCommands)
            {
                if (_lastMoveNextResult)
                {
                    OnCommandComplete(this);
                }
                
                return _lastMoveNextResult = false;
            }

            if (anyActiveParallelCommands)
            {
                MoveNextParallelCommands();
            }

            if (anyQueuedCommands)
            {
                MoveNextQueuedCommand();
            }

            return _lastMoveNextResult = true;
        }

        /// <summary>
        ///     Moves all active parallel commands forward one step. Removes completed commands from
        ///     <see cref="_activeParallelCommands" />
        /// </summary>
        private void MoveNextParallelCommands()
        {
            IEnumerable<CommandData> activeCommands = new List<CommandData>(_activeParallelCommands);

            foreach (CommandData command in activeCommands)
            {
                if (!command.MoveNext())
                {
                    _activeParallelCommands.Remove(command);
                }
            }
        }

        /// <summary>
        ///     Processes the next command in <see cref="_commands" /> If a parallel command is encountered it is transferred
        ///     to <see cref="_activeParallelCommands" /> and run from there.
        /// </summary>
        private void MoveNextQueuedCommand()
        {
            CommandData queuedCommandData = _commands[_currCommandIndex];
                        
            if (queuedCommandData.CommandMode == CommandMode.Serial)
            {
                if (!queuedCommandData.MoveNext())
                {
                    _currCommandIndex++;
                }
            }
            else //parallel command
            {
                _activeParallelCommands.Add(queuedCommandData);
                _currCommandIndex++;
            }
        }

        /// <summary>
        ///     Stops and rewinds the queue.
        /// </summary>
        public void Reset()
        {
            _shouldReset = true;
        }

        public void Complete()
        {
            _currCommandIndex = _commands.Count;
            _activeParallelCommands.Clear();
            OnCommandComplete(this);
        }

        /// <summary>
        ///     Enqueues a command that does not block subsequent commands. However, the enumerator must finish in order for
        ///     this command queue to finish.
        /// </summary>
        public void AddParallel(IEnumerator enumerator)
        {
            _commands.Add(new CommandData(enumerator, CommandMode.Parallel));
        }

        public void AddParallel(Func<IEnumerator> enumeratorFunc)
        {
            _commands.Add(new CommandData(enumeratorFunc, CommandMode.Parallel));
        }

        /// <summary>
        ///     Add a blocking command to the queue.
        /// </summary>
        public void AddSerial(IEnumerator enumerator)
        {
            _commands.Add(new CommandData(enumerator, CommandMode.Serial));
        }

        /// <summary>
        ///     Adds a one-shot action to the queue. The action self-evidently not block subsequent commands, but will be
        ///     blocked by previous commands, like any other command.
        /// </summary>
        public void AddSerial(Action action)
        {
            _commands.Add(new CommandData(action));
        }

        public void AddSerial(Func<IEnumerator> enumeratorFunc)
        {
            _commands.Add(new CommandData(enumeratorFunc, CommandMode.Serial));
        }

        /// <summary>
        ///     Resets the <see cref="_currCommandIndex" /> and clears the list of active parallel commands.
        /// </summary>
        private bool DoReset()
        {
            bool shouldReset = _shouldReset;
            if (shouldReset)
            {
                _currCommandIndex = 0;
                _activeParallelCommands.Clear();
                _shouldReset = false;
            }
            return shouldReset;
        }

        /// <summary>
        ///     A private wrapper on <see cref="IEnumerator" /> that consolidates serial commands, parallel commands, and
        ///     actions.
        /// </summary>
        private class CommandData : IEnumerator
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

            public CommandData(Action action)
            {
                _action = action;
                CommandMode = CommandMode.Serial; //it's a one-shot, but arbitrarily make it serial
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

        private enum CommandMode
        {
            Serial,
            Parallel
        }
    }
}
