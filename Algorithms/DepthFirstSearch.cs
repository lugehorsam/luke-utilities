namespace Utilities.Algorithms
{
    using System.Collections;
    using System.Collections.Generic;

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

        private IEnumerator<INode<T>> Execute(INode<T> node)
        {
            yield return node;

            foreach (DirectedEdge<T> edge in node.Edges)
            {
                INode<T> end = edge.End;
                if (end == null)
                {
                    Diag.Warn("Incomplete edge " + edge);
                }
                else
                {
                    yield return end;
                }

                INode<T> next = Execute(end).Current;

                if (next != null)
                {
                    yield return next;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
