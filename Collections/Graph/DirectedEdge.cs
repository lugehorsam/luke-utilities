using System.Collections;
using System.Collections.Generic;

namespace Utilities
{
	public class DirectedEdge<T> : IDirectedEdge<T>
	{
		void UpdateNodeEdgeList(INode<T> element)
		{
			element.Edges.Add(this);
		}		

		public bool Contains(T element)
		{
			return Start.Value.Equals(element) || End.Value.Equals(element);
		}

		public INode<T> OtherElement(T element)
		{
			return element.Equals(Start.Value) ? End: Start; 
		}
		
		public INode<T> Start { get; }
		
		public INode<T> End { get; }
					
		public DirectedEdge(T start, T end)
		{
			Start = new Node<T>(start);
			End = new Node<T>(end);
			
			UpdateNodeEdgeList(Start);
			UpdateNodeEdgeList(End);
		}		
		
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}				

		public bool DirectsTo(DirectedEdge<T> otherDirectedEdge)
		{
			return End.Equals(otherDirectedEdge.Start);
		}

		public IEnumerator<INode<T>> GetEnumerator()
		{
			yield return Start;
			yield return End;
		}

		public int CompareTo(DirectedEdge<T> other)
		{
			if (DirectsTo(other))
			{
				return -1;
			} 
			
			if (other.DirectsTo(this))
			{
				return 1;
			}		
			
			return 0;
		}

		public override string ToString()
		{
			return this.ToString("Start " + Start, "end " + End);
		}				
	}
}
