namespace Utilities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class DepthFirstSearch<T> : IEnumerator<T> where T : class
    {
        public delegate void LevelRevertHandler(T oldNode, int oldLevel, T newNode, int newLevel);
        public event LevelRevertHandler OnLevelRevert = delegate { };

        public T Current => _currentSearchData.Vertex;
        public List<T> VisitedNodes => _visitedNodes;
        
        private SearchData _currentSearchData;
        private readonly Graph<T> _graph;
        private readonly List<T> _visitedNodes = new List<T>();
        private readonly Stack<SearchData> _pendingSearches = new Stack<SearchData>();

        object IEnumerator.Current => Current;

        public DepthFirstSearch(Graph<T> graph, T start)
        {
            _graph = graph;

            if (!graph.Nodes.Contains(start))
            {
                throw new Exception($"Node {start} was not found in graph {graph}.");
            }

            _pendingSearches.Push(new SearchData(start, 0));
        }
        
        public bool MoveNext()
        {
            return !AnyVerticesLeft() && DoSearch();
        }
        
        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private void AssignCurrentVertex()
        {
            int oldLevel = _currentSearchData.Level;
            T oldVertex = _currentSearchData.Vertex;
            
            _currentSearchData = _pendingSearches.Pop();

            if (_currentSearchData.Level < oldLevel)
            {
                OnLevelRevert(oldVertex, oldLevel, _currentSearchData.Vertex, _currentSearchData.Level);
            }
        }

        private bool AnyVerticesLeft()
        {
            return _pendingSearches.Any();
        }

        private bool DoSearch()
        {
            AssignCurrentVertex();

            if (_visitedNodes.Contains(Current))
            {
                return MoveNext();
            }

            _visitedNodes.Add(Current);

            PushNeighborsToStack();

            return true;
        }

        private void PushNeighborsToStack()
        {
            IEnumerable<T> neighborList = _graph[Current];
            
            foreach (T neighbor in neighborList)
            {
                if (!_visitedNodes.Contains(neighbor))
                {
                    _pendingSearches.Push(new SearchData(neighbor, _currentSearchData.Level + 1));                    
                }
            }
        }
                
        private class SearchData
        {
            public T Vertex { get; }
            public int Level { get; }

            public SearchData(T vertex, int level)
            {
                Vertex = vertex;
                Level = level;
            }
        }
    }
}
