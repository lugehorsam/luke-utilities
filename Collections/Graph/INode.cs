using System;
using System.Collections.Generic;

namespace Utilities
{    
    public interface INode<T>
    {    
        List<DirectedEdge<T>> Edges { get; }
        T Value { get; }
    }
}
