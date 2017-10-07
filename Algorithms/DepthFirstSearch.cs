using System.Collections;
using System.Collections.Generic;

namespace Utilities.Algorithms
{
	public class DepthFirstSearch<T> : IEnumerable<INode<T>>
	{
		private readonly INode<T> _nodeToSearchFrom;
		
		public DepthFirstSearch(INode<T> nodeToSearchFrom)
		{
			_nodeToSearchFrom = nodeToSearchFrom;
		}
		
		public IEnumerator<INode<T>> GetEnumerator()
		{
			return Execute(_nodeToSearchFrom);
		}

		IEnumerator<INode<T>> Execute(INode<T> node)
		{		
			yield return node;
			foreach (var edge in node.Edges)
			{
				var end = edge.End;
				if (end == null)
				{
					Diag.Warn("Incomplete edge " + edge);
				}
				else
				{
					yield return end;
				}
				
				var next = Execute(end).Current;	
				
				if (next != null)
					yield return next;
			}
		}
		
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
