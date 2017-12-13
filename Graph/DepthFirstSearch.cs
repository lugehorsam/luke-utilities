namespace Utilities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class DepthFirstSearch<T> : IEnumerator<T>
    {
        private readonly Graph<T> _graph;
        private readonly HashSet<T> _visitedNodes = new HashSet<T>();
        private readonly Stack<T> _stack = new Stack<T>();
        private readonly bool _breakOnCycle;
        
        private T _current;

        public bool BrokeOnCycle { get; private set; }
                
        public T Current { get { return _current; }}
        
        public IEnumerable<T> CurrentComponent { get { return _visitedNodes; }}
        
        object IEnumerator.Current
        {
            get { return _current; }
        }
        
        public DepthFirstSearch(Graph<T> graph, T start, bool breakOnCycle)
        {
            _graph = graph;
            _current = start;
            _breakOnCycle = breakOnCycle;
            
            if (!graph.Nodes.Contains(start))
            {
                throw new Exception($"Node {start} was not found in graph {graph}.");
            }
            
            _stack.Push(_current);
        }
        
        public bool MoveNext()
        {
            bool anyOnStack = _stack.Any();

            if (!anyOnStack)
            {
                return false;
            }
            
            T vertex = _stack.Pop();

            if (_visitedNodes.Contains(vertex))
            {
                BrokeOnCycle = _breakOnCycle;
                return !_breakOnCycle && MoveNext();
            }

            _visitedNodes.Add(vertex);

            _current = vertex;
                
            foreach (T neighbor in _graph[vertex])
            {
                if (!_visitedNodes.Contains(neighbor))
                {
                    _stack.Push(neighbor);
                }
            }

            return true;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
      

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
