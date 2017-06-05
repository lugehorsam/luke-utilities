using System;
using System.Linq;
using System.Collections.Generic;

namespace Utilities
{	
	public static class GraphUtility {
			
		public static List<List<TEdge>> GetConnectedComponents<TElement, TEdge>(IEnumerable<TEdge> edges) 
			where TEdge : IDirectedEdge<TElement> 
		{
			
			var edgesArray = edges.ToArray();
			Array.Sort(edgesArray);
			
			var visitedNodes = new HashSet<INode<TElement>>();
			INode<TElement>[] distinctNodes = edgesArray.SelectMany<TEdge, INode<TElement>>(edge => edge).Distinct().ToArray();
			
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
						visitedNodes.Add(searchedNode);
						if (lastNode != null && !lastNode.Equals(default(INode<TElement>)))
						{
							var dfsEdge = edgesArray.First(edge => edge.Start.Equals(lastNode) && edge.End.Equals(searchedNode));
							currentComponent.Add(dfsEdge);
						}
						lastNode = searchedNode;

					}					
					
					connectedComponents.Add(currentComponent);
				}
			}
			
			return connectedComponents;
		}				
	}
}
