using System;
using System.Collections.Generic;

namespace Utilities
{
	public interface IDirectedEdge<T> : IEnumerable<INode<T>>, IComparable<DirectedEdge<T>>
	{			
		INode<T> Start { get; }	
		INode<T> End { get; }
	}	
}
