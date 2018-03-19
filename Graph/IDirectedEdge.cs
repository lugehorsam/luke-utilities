namespace Utilities
{
    using System;
    using System.Collections.Generic;

    public interface IDirectedEdge<T> : IEnumerable<T>, IEnumerable<INode<T>>, IComparable<DirectedEdge<T>>
    {
        INode<T> Start { get; }
        INode<T> End { get; }
    }
}
