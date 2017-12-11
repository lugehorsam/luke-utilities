namespace Utilities
{
    using System;
    using System.Collections.Generic;

    public class Graph<T>
    {
        public IEnumerable<T> this[T node]
        {
            get { return _adjacencyList[node]; }
        }
        
        public Graph() { }

        public Graph(IEnumerable<T> vertices, IEnumerable<Tuple<T, T>> edges)
        {
            foreach (T vertex in vertices)
            {
                AddVertex(vertex);
            }

            foreach (Tuple<T, T> edge in edges)
            {
                AddEdge(edge);
            }
        }

        private readonly Dictionary<T, HashSet<T>> _adjacencyList = new Dictionary<T, HashSet<T>>();
        
        public IEnumerable<T> Nodes { get { return _adjacencyList.Keys; }}

        public void AddVertex(T vertex)
        {
            _adjacencyList[vertex] = new HashSet<T>();
        }

        public void AddEdge(Tuple<T, T> edge)
        {
            if (_adjacencyList.ContainsKey(edge.Item1) && _adjacencyList.ContainsKey(edge.Item2))
            {
                _adjacencyList[edge.Item1].Add(edge.Item2);
                _adjacencyList[edge.Item2].Add(edge.Item1);
            }
        }
    }
}
