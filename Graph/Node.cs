namespace Utilities
{
    using System.Collections.Generic;

    public class Node<T> : INode<T>
    {
        public Node(T value)
        {
            Value = value;
            Edges = new List<DirectedEdge<T>>();
        }

        public List<DirectedEdge<T>> Edges { get; }
        public T Value { get; }

        public override string ToString()
        {
            return this.ToString(Value);
        }
    }
}
