namespace Utilities.Algorithms
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class BreadthFirstSearch<T>
    {
        private readonly HashSet<INode<T>> _searchedNodes = new HashSet<INode<T>>();
        private readonly INode<T> _sourceNode;

        public BreadthFirstSearch(INode<T> sourceNode)
        {
            _sourceNode = sourceNode;
        }

        public ReadOnlyCollection<INode<T>> SearchedNodes
        {
            get { return new ReadOnlyCollection<INode<T>>(_searchedNodes.ToList()); }
        }

        public void Execute()
        {
            ExecuteOneLevel().Run();
        }

        public IEnumerator ExecuteOneLevel()
        {
            var nextNodes = new Queue<INode<T>>();
            nextNodes.Enqueue(_sourceNode);

            do
            {
                INode<T> node = nextNodes.Dequeue();

                if (node == null)
                {
                    yield break;
                }

                foreach (DirectedEdge<T> edge in node.Edges)
                {
                    if (edge.Start.Equals(node))
                    {
                        nextNodes.Enqueue(edge.End);
                    }
                }

                _searchedNodes.Add(node);

                yield return null;
            }
            while (true);
        }
    }
}
