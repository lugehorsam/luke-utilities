namespace Utilities
{ 
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Holds a queue of enumerators and executes them either in serial or parallel.
    /// </summary>
    public class CommandQueue : IEnumerator
    {
        /// <summary>
        /// A list of all serial and parallel commands in the queue. Commands are never removed from this list.
        /// Instead the <see cref="_currCommandIndex"/> increments as it runs commands in sequence.
        /// </summary>
        private readonly List<Command> _commands = new List<Command>();
        
        /// <summary>
        /// All active parallel commands placed from <see cref="_commands"/> that have not yet completed.
        /// </summary>
        private readonly List<Command> _activeParallelCommands = new List<Command>();
        
        /// <summary>
        /// The index of the last run command from <see cref="_commands"/>.
        /// </summary>
        private int _currCommandIndex;
        
        /// <summary>
        /// Whether or not this command queue should reset itself.
        /// </summary>
        private bool _shouldReset;
        
        /// <summary>
        /// Unity's Coroutine system handles this property in a special manner. It's best not to deal with this and
        /// just return null instead.
        /// See details in https://docs.unity3d.com/ScriptReference/CustomYieldInstruction.html
        /// </summary>
        public object Current => null;

        /// <summary>
        /// Calls <see cref="MoveNext"/> on the active parallel commands and the active serial command. Only returns
        /// false if there are no serial and no parallel enumerators left, or if the queue has been <see cref="Reset"/>.
        /// </summary>
        public bool MoveNext()
        {
            if (DoReset())
            {
                return false;
            }
            
            bool anyQueuedCommands = _currCommandIndex < _commands.Count;
            bool anyParallelCommands = _activeParallelCommands.Any();
            
            if (!anyQueuedCommands && !anyParallelCommands)
            {
                return false;
            }
            
            if (anyParallelCommands)
            {
                MoveNextParallelCommands();
            }
            
            if (anyQueuedCommands)
            {
                MoveNextQueuedCommand();
            }
            
            return true;
        }
        
        /// <summary>
        /// Moves all active parallel commands forward one step. Removes completed commands from
        /// <see cref="_activeParallelCommands"/>
        /// </summary>
        private void MoveNextParallelCommands()
        {
            IEnumerable<Command> activeCommands = new List<Command>(_activeParallelCommands);
            foreach (var command in activeCommands)
            {
                if (!command.MoveNext())
                    _activeParallelCommands.Remove(command);
            }
        }
        
        /// <summary>
        /// Processes the next command in <see cref="_commands"/> If a parallel command is encountered it is transferred
        /// to <see cref="_activeParallelCommands"/> and run from there.
        /// </summary>
        private void MoveNextQueuedCommand()
        {
            Command queuedCommand = _commands[_currCommandIndex];
            if (queuedCommand.CommandMode == CommandMode.Serial)
            {
                if (!queuedCommand.MoveNext())
                    _currCommandIndex++;
            }
            else //parallel command
            {
                _activeParallelCommands.Add(queuedCommand);
                _currCommandIndex++;
            }
        }
        
        /// <summary>
        /// Stops and rewinds the queue.
        /// </summary>
        public void Reset()
        {
            _shouldReset = true;
        }
        
        /// <summary>
        /// Enqueues a command that does not block subsequent commands. However, the enumerator must finish in order for
        /// this command queue to finish.
        /// </summary>
        public void AddParallel(IEnumerator enumerator)
        {
            _commands.Add(new Command(enumerator, CommandMode.Parallel));
        }
        
        /// <summary>
        /// Add a blocking command to the queue.
        /// </summary>
        public void AddSerial(IEnumerator enumerator)
        {
            _commands.Add(new Command(enumerator, CommandMode.Serial));
        }
        
        /// <summary>
        /// Adds a one-shot action to the queue. The action self-evidently not block subsequent commands, but will be
        /// blocked by previous commands, like any other command.
        /// </summary>
        public void AddSerial(Action action)
        {
            _commands.Add(new Command(action));
        }
        
        /// <summary>
        /// Resets the <see cref="_currCommandIndex"/> and clears the list of active parallel commands.
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
        /// A private wrapper on <see cref="IEnumerator"/> that consolidates serial commands, parallel commands, and
        /// actions.
        /// </summary>
        private class Command : IEnumerator
        {
            private readonly IEnumerator _enumerator;
            
            private readonly Action _action;
            
            public object Current => _enumerator?.Current;

            public CommandMode CommandMode { get; }
            
            public Command(IEnumerator enumerator, CommandMode commandMode)
            {
                _enumerator = enumerator;
                CommandMode = commandMode;
            }
            
            public Command(Action action)
            {
                _action = action;
                CommandMode = CommandMode.Serial; //it's a one-shot, but arbitrarily make it serial
            }
            
            public bool MoveNext()
            {
                if (_action != null)
                {
                    _action();
                    return false;
                }
                if (_enumerator != null)
                    return _enumerator.MoveNext();
                
                return false;
            }
            
            public void Reset()
            {
                _enumerator.Reset();
            }
        }
        

    }
}
