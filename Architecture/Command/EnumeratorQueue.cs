using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{ 
    public class EnumeratorQueue : IEnumerator, IEnumeratorQueue
    {
        public int Count 
        {
            get {
                return _nextEnumerators.Count;
            }
        }

        public object Current
        {
            get { return _currentEnumerator; }
        }

        private readonly LinkedList<EnumeratorData> _nextEnumerators = new LinkedList<EnumeratorData>();
        private readonly Stack<EnumeratorData> _oldEnumerators = new Stack<EnumeratorData>();

        private EnumeratorData _currentEnumerator;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private readonly List<IEnumerator> _parallelEnumerators = new List<IEnumerator>();

        public bool MoveNext ()
        {
            bool isParallelEnumerator = TryUpdateParallelEnumerators();
            if (_nextEnumerators.First == null)
                return isParallelEnumerator;

            _currentEnumerator = _nextEnumerators.First.Value;
            Debug.Log("next enumerators count is " + _nextEnumerators.Count);
            if (_currentEnumerator.CommandMode == CommandMode.Serial)
            {
                if (_currentEnumerator.MoveNext())
                {
                    Diagnostics.Log("blocking on serial enumerator");

                    return true;
                }
                
                MoveEnumeratorToStack (_currentEnumerator);
                Diagnostics.Log("not moving next " + isParallelEnumerator);
                return isParallelEnumerator;                      
            }
            else
            {
                _nextEnumerators.RemoveFirst();
                Diagnostics.Log("adding parallel enuerator");
                _parallelEnumerators.Add(_currentEnumerator);
                return true;
            }                        
        }

        bool TryUpdateParallelEnumerators()
        {
            bool updatedAtLeastOne = false;
            var enumeratorsToUpdate = new List<IEnumerator>(_parallelEnumerators);
            foreach (var enumerator in enumeratorsToUpdate)
            {
                if (enumerator.MoveNext())
                {
                    Diagnostics.Log("updated parallel enumerator");
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

        public void AddSerial (IEnumerator enumerator)
        {
            Debug.Log("adding serial enumerator ");
            _nextEnumerators.AddLast (new EnumeratorData(enumerator, CommandMode.Serial));
        }

        public void AddParallel(Action action)
        {
            _nextEnumerators.AddLast
            (
                new EnumeratorData(ActionWrapper(action),CommandMode.Parallel)
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
            public object Current
            {
                get
                {
                    return _enumerator.Current;
                }
            }
            
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
