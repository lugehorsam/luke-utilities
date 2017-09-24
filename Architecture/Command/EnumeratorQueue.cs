using System;
using System.Collections;
using System.Collections.Generic;

namespace Utilities
{ 
    public class EnumeratorQueue : IEnumerator
    {
        private readonly LinkedList<EnumeratorData> _nextEnumerators = new LinkedList<EnumeratorData>();
        private readonly Stack<EnumeratorData> _oldEnumerators = new Stack<EnumeratorData>();
        private readonly List<EnumeratorData> _parallelEnumerators = new List<EnumeratorData>();

        private EnumeratorData _currentEnumerator;
        
        public int Count => _nextEnumerators.Count + _parallelEnumerators.Count;
        public object Current => _currentEnumerator;
        public string Id { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext ()
        {
            var isParallelEnumerator = TryUpdateParallelEnumerators();
            
            if (_nextEnumerators.First == null)
                return isParallelEnumerator;

            _currentEnumerator = _nextEnumerators.First.Value;
            
            if (_currentEnumerator.CommandMode == CommandMode.Serial)
            {
                Diag.Log("current enumerator " + _currentEnumerator);
                if (_currentEnumerator.MoveNext())
                {
                    Diag.Log("returning true");
                    return true;
                }
                
                Diag.Log("moving to stack");
                MoveEnumeratorToStack (_currentEnumerator);
                return MoveNext() || isParallelEnumerator;
            }
            
            _nextEnumerators.RemoveFirst();
            
            _parallelEnumerators.Add(_currentEnumerator);
            MoveNext();
            return true;                 
        }

        public override string ToString()
        {
            var str = base.ToString();

            if (Id != null)
                str += Id;

            return str;
        }

        private bool TryUpdateParallelEnumerators()
        {
            var updatedAtLeastOne = false;
            
            var enumeratorsToUpdate = new List<EnumeratorData>(_parallelEnumerators);
            
            foreach (var enumerator in enumeratorsToUpdate)
            {
                if (enumerator.MoveNext())
                {
                    updatedAtLeastOne = true;
                }
                else
                {                    
                    _parallelEnumerators.Remove(enumerator);
                    _oldEnumerators.Push(enumerator);
                }
            }
            
            return updatedAtLeastOne;
        }
        
        public void Reset ()
        {
            while (_oldEnumerators.Count > 0)
            {
                _nextEnumerators.AddFirst(_oldEnumerators.Pop());
            }
        }

        public void AddSerial(Func<IEnumerator> func)
        {
            _nextEnumerators.AddLast
            (
                new EnumeratorData(func, CommandMode.Serial)
            );
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
        
        void MoveEnumeratorToStack (EnumeratorData enumerator)
        {
            _nextEnumerators.RemoveFirst ();
            _oldEnumerators.Push (enumerator);
        }

        public void StopCurrentEnumerator ()
        {
            if (_nextEnumerators.First != null) 
            {
                MoveEnumeratorToStack (_nextEnumerators.First.Value);
            }
        }

        private class EnumeratorData : IEnumerator
        {
            public object Current => _enumerator.Current;

            public CommandMode CommandMode { get; }

            private IEnumerator _enumerator;
            private readonly Func<IEnumerator> _enumeratorFunc;

            public EnumeratorData(IEnumerator enumerator, CommandMode commandMode)
            {
                _enumerator = enumerator;
                CommandMode = commandMode;
            }
            
            public EnumeratorData(Func<IEnumerator> enumerator, CommandMode commandMode)
            {
                _enumerator = null;
                CommandMode = commandMode;
                _enumeratorFunc = enumerator;
            }
            
            public bool MoveNext()
            {
                if (_enumerator != null)
                    return _enumerator.MoveNext();

                if (_enumeratorFunc != null)
                {
                    _enumerator = _enumeratorFunc();
                    return MoveNext();
                }

                return false;
            }

            public void Reset()
            {
                _enumerator.Reset();
            }

            public override string ToString()
            {
                return _enumerator.ToString();
            }
        }
    }   
}
