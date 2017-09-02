﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Utilities
{ 
    public class EnumeratorQueue : IEnumerator, IEnumeratorQueue
    {
        private readonly LinkedList<EnumeratorData> _nextEnumerators = new LinkedList<EnumeratorData>();
        private readonly Stack<EnumeratorData> _oldEnumerators = new Stack<EnumeratorData>();
        private readonly List<IEnumerator> _parallelEnumerators = new List<IEnumerator>();

        private EnumeratorData _currentEnumerator;
        
        public int Count => _nextEnumerators.Count + _parallelEnumerators.Count;
        public object Current => _currentEnumerator;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext ()
        {
            bool isParallelEnumerator = TryUpdateParallelEnumerators();
            
            if (_nextEnumerators.First == null)
                return isParallelEnumerator;

            _currentEnumerator = _nextEnumerators.First.Value;
            
            if (_currentEnumerator.CommandMode == CommandMode.Serial)
            {
                if (_currentEnumerator.MoveNext())
                {
                    return true;
                }
                
                MoveEnumeratorToStack (_currentEnumerator);
                return MoveNext() || isParallelEnumerator;
            }
            
            _nextEnumerators.RemoveFirst();
            
            _parallelEnumerators.Add(_currentEnumerator);
            MoveNext();
            return true;                 
        }

        bool TryUpdateParallelEnumerators()
        {
            bool updatedAtLeastOne = false;
            
            var enumeratorsToUpdate = new List<IEnumerator>(_parallelEnumerators);
            
            foreach (var enumerator in enumeratorsToUpdate)
            {
                if (enumerator.MoveNext())
                {
                    updatedAtLeastOne = true;
                }
                else
                {                    
                    _parallelEnumerators.Remove(enumerator); //TODO move it to the stack directly afterwards
                }
            }
            
            return updatedAtLeastOne;
        }
        
        public void Reset ()
        {
            throw new NotImplementedException ();
        }

        public void AddSerial(Action action)
        {
            _nextEnumerators.AddLast
            (
                new EnumeratorData(ActionWrapper(action), CommandMode.Serial)
            );
        }

        public void AddSerial (IEnumerator enumerator)
        {
            _nextEnumerators.AddLast (new EnumeratorData(enumerator, CommandMode.Serial));
        }

        public void AddParallel(Action action)
        {
            _nextEnumerators.AddLast
            (
                new EnumeratorData(ActionWrapper(action), CommandMode.Parallel)
            );
        }

        public void AddParallel(IEnumerator enumerator)
        {
            _nextEnumerators.AddLast(new EnumeratorData(enumerator, CommandMode.Parallel));
        }

        IEnumerator ActionWrapper(Action action)
        {            
            action();
            yield return null;
        }
        
        public void AddRange(IEnumerable<IEnumerator> enumerators)
        {
            foreach (IEnumerator enumerator in enumerators)
            {
                AddSerial(enumerator);
            }
        }

        void MoveEnumeratorToStack (EnumeratorData enumerator)
        {
            _nextEnumerators.RemoveFirst ();
            _oldEnumerators.Push (enumerator);
        }

        public void StopCurrentEnumerator ()
        {
            if (_nextEnumerators.First != null) {
                MoveEnumeratorToStack (_nextEnumerators.First.Value);
            }
        }

        private class EnumeratorData : IEnumerator
        {
            public object Current => _enumerator.Current;

            public CommandMode CommandMode { get; }

            private readonly IEnumerator _enumerator;

            public EnumeratorData(IEnumerator enumerator, CommandMode commandMode)
            {
                _enumerator = enumerator;
                CommandMode = commandMode;
            }
            
            public bool MoveNext()
            {
                return _enumerator.MoveNext();
            }

            public void Reset()
            {
                _enumerator.Reset();
            }
        }
    }   
}
