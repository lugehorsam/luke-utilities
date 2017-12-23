namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Graph<T>
    {
        public IEnumerable<T> this[T node] => _adjacencyList[node];

        public Graph() { }

        public Graph(IEnumerable<T> vertices, IEnumerable<Tuple<T, T>> edges)
        {
            foreach (T vertex in vertices)
            {
                AddVertex(vertex);
            }

            foreach (Tuple<T, T> edge in edges)
            {
                AddEdge(edge.Item1, edge.Item2);
            }
        }

        private readonly Dictionary<T, HashSet<T>> _adjacencyList = new Dictionary<T, HashSet<T>>();

        public IEnumerable<T> Nodes => _adjacencyList.Keys;

        public void AddVertex(T vertex)
        {
            if (_adjacencyList.ContainsKey(vertex))
            {
                throw new InvalidOperationException($"Attempted to add duplicate vertex {vertex}");
            }

            _adjacencyList[vertex] = new HashSet<T>();
        }

        public void RemoveVertex(T vertex)
        {
            _adjacencyList.Remove(vertex);
            
            foreach (var existingVertex in _adjacencyList.Keys)
            {
                if (_adjacencyList[existingVertex].Contains(vertex))
                {
                    _adjacencyList[existingVertex].Remove(vertex);
                }    
            }
        }

        public void AddEdge(T item1, T item2)
        {
            if (!_adjacencyList.ContainsKey(item1))
            {
                AddVertex(item1);
            }

            if (!_adjacencyList.ContainsKey(item2))
            {
                AddVertex(item2);
            }

            _adjacencyList[item1].Add(item2);
            _adjacencyList[item2].Add(item1);
        }

        public List<Tuple<T, T>> GetEdgesBetween(IEnumerable<T> nodes)
        {
            var connectingEdges = new List<Tuple<T, T>>();

            foreach (T node in nodes)
            {
                IEnumerable<T> connectedNodes = _adjacencyList[node].Where(nodes.Contains);
                
                IEnumerable<Tuple<T, T>> connectedEdges =
                        connectedNodes.Select(connectedNode => new Tuple<T, T>(node, connectedNode));
                connectingEdges.AddRange(connectedEdges);
            }

            return connectingEdges;
        }

        public bool IsEdgeBetween(T node1, T node2)
        {
            return _adjacencyList[node1].Contains(node2);
        }
    }
}
