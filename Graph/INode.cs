namespace Utilities
{
    using System.Collections.Generic;

    public interface INode<T>
    {
        List<DirectedEdge<T>> Edges { get; }
        T Value { get; }
    }
}
