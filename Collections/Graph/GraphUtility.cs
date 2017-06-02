using System;
using System.Linq;
using System.Collections.Generic;

namespace Utilities
{	
	public static class GraphUtility {
			
		public static List<List<TEdge>> GetConnectedComponents<TElement, TEdge>(IEnumerable<TEdge> edges) 
			where TEdge : IDirectedEdge<TElement> 
		{			
			var visitedNodes = new HashSet<INode<TElement>>();
			INode<TElement>[] distinctNodes = edges.SelectMany(edge => edge).Distinct().ToArray();

			Array.Sort(distinctNodes);
			
			var connectedComponents = new List<List<TEdge>>();
			
			foreach (var node in distinctNodes)
			{
				if (visitedNodes.Add(node))
				{
					var currentComponent = new List<TEdge>();

					var dfs = new DepthFirstSearch<TElement>(node);
					INode<TElement> lastNode = default(INode<TElement>);
					foreach (INode<TElement> searchedNode in dfs)
					{
						visitedNodes.Add(node);
						if (!lastNode.Equals(default(INode<TElement>)))
							currentComponent.Add(edges.First(edge => edge.Start.Equals(lastNode) && edge.End.Equals(searchedNode)));
						lastNode = searchedNode;

					}					
					
					connectedComponents.Add(currentComponent);
				}
			}

			return connectedComponents;
		}		
	}
}
