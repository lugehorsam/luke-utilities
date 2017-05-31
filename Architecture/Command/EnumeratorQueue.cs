using System;
using System.Collections;
using System.Collections.Generic;

namespace Utilities
{ 
    public class EnumeratorQueue : IEnumerator, IEnumeratorQueue
    {
        public int Count 
        {
            get {
                return nextEnumerators.Count;
            }
        }

        public object Current
        {
            get { return currentEnumerator; }
        }

        readonly LinkedList<EnumeratorData> nextEnumerators = new LinkedList<EnumeratorData>();
        readonly Stack<EnumeratorData> oldEnumerators = new Stack<EnumeratorData>();

        private EnumeratorData currentEnumerator;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext ()
        {
            if (nextEnumerators.First == null)
                return false;

            currentEnumerator = nextEnumerators.First.Value;

            if (!currentEnumerator.MoveNext ()) {
                MoveEnumeratorToStack (currentEnumerator);
                return MoveNext();
            } 
                        
            return true;
        }

        public void Reset ()
        {
            throw new NotImplementedException ();
        }

        public void AddSerial (IEnumerator enumerator)
        {
            nextEnumerators.AddLast (new EnumeratorData(enumerator, CommandMode.Serial));
        }

        public void AddParallel(Action action)
        {
            nextEnumerators.AddLast
            (
                new EnumeratorData(ActionWrapper(action),CommandMode.Parallel)
            );
        }

        public void AddParallel(IEnumerator enumerator)
        {
            nextEnumerators.AddLast(new EnumeratorData(enumerator, CommandMode.Parallel));
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
            nextEnumerators.RemoveFirst ();
            oldEnumerators.Push (enumerator);
        }

        public void StopCurrentEnumerator ()
        {
            if (nextEnumerators.First != null) {
                MoveEnumeratorToStack (nextEnumerators.First.Value);
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
