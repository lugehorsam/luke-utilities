using System;
using System.Collections.Generic;

namespace Utilities
{
    public class Node<T> : INode<T>
    {
        public List<DirectedEdge<T>> Edges { get; }
        public T Value { get; }

        public Node(T value)
        {
            Value = value;
            Edges = new List<DirectedEdge<T>>();
        }

    }
}
