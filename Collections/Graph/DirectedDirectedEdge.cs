using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Utilities
{
	public class DirectedEdge<T> : IEnumerable<INode<T>>, IComparable<DirectedEdge<T>>
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

		public static List<DirectedEdge<T>> GetEdgesWithElement(IEnumerable<DirectedEdge<T>> edges, T element)
		{
			var edgesWithNode = new List<DirectedEdge<T>>();
		
			foreach (var connection in edges)
			{
				if (connection.Contains(element))
					edgesWithNode.Add(connection);
			}
			return edgesWithNode;
		}
	
		public static List<DirectedEdge<T>> GetIncompleteEdges(IEnumerable<DirectedEdge<T>> edges)
		{
			var connectionsWithOutNode = new List<DirectedEdge<T>>();
		
			foreach (var connection in edges)
			{
				if (connection.Start == null || connection.End == null)
					connectionsWithOutNode.Add(connection);
			}

			return connectionsWithOutNode;
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
		
		public static List<List<DirectedEdge<T>>> GetConnectedComponents(IEnumerable<DirectedEdge<T>> edges)
		{
			var visitedNodes = new HashSet<INode<T>>();
			var distinctNodes = GetDistinctNodes(edges).ToArray();
			Array.Sort(distinctNodes);
			
			var connectedComponents = new List<List<DirectedEdge<T>>>();
			
			foreach (var node in distinctNodes)
			{
				if (visitedNodes.Add(node))
				{
					var currentComponent = new List<DirectedEdge<T>>();

					var dfs = new DepthFirstSearch<T>(node);
					INode<T> lastNode = default(INode<T>);
					foreach (INode<T> searchedNode in dfs)
					{
						visitedNodes.Add(node);
						if (!lastNode.Equals(default(T)))
							currentComponent.Add(edges.First(edge => edge.Start.Equals(lastNode) && edge.End.Equals(searchedNode)));
						lastNode = searchedNode;

					}					
					
					connectedComponents.Add(currentComponent);
				}
			}

			return connectedComponents;
		}

		public static IEnumerable<INode<T>> GetDistinctNodes(IEnumerable<DirectedEdge<T>> edges)
		{
			return edges.SelectMany(edge => edge).Distinct();
		}		
	}
}
