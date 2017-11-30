namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Algorithms;

    public static class GraphExt
    {
        public static List<TComponent> GetComponents<TComponent, TEdge, TElement>(IEnumerable<TEdge> edges)
            where TComponent : List<TEdge>, new() where TEdge : IDirectedEdge<TElement>
        {
            TEdge[] edgesArray = edges.ToArray();
            Array.Sort(edgesArray);

            var visitedNodes = new HashSet<INode<TElement>>();
            INode<TElement>[] distinctNodes =
                    edgesArray.SelectMany<TEdge, INode<TElement>>(edge => edge).Distinct().ToArray();

            var connectedComponents = new List<TComponent>();

            foreach (INode<TElement> node in distinctNodes)
            {
                if (visitedNodes.Add(node))
                {
                    var currentComponent = new TComponent();

                    var dfs = new DepthFirstSearch<TElement>(node);
                    INode<TElement> lastNode = default(INode<TElement>);
                    foreach (INode<TElement> searchedNode in dfs)
                    {
                        visitedNodes.Add(searchedNode);
                        if ((lastNode != null) && !lastNode.Equals(default(INode<TElement>)))
                        {
                            TEdge dfsEdge =
                                    edgesArray.First(edge => edge.Start.Equals(lastNode) &&
                                                             edge.End.Equals(searchedNode));
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
