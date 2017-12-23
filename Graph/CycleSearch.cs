namespace Utilities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class CycleSearch<T> : IEnumerator<T> where T : class
    {
        private readonly Graph<T> _graph;
        private readonly ComponentSearch<T> _componentSearch;
               
        public T Current { get; private set; }

        object IEnumerator.Current => Current;
       
        public CycleSearch(Graph<T> graph)
        {
            _graph = graph;
            _componentSearch = new ComponentSearch<T>(_graph);
        }

        public bool MoveNext()
        {
            Current = _componentSearch.Current;
            return _componentSearch.MoveNext();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetCycles()
        {
            throw new NotImplementedException();
        }
    }
}
