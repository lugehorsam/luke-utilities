using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Utilities.Algorithms
{
    public class BreadthFirstSearch<T>
    {
        private readonly INode<T> _sourceNode;
        
        public ReadOnlyCollection<INode<T>> SearchedNodes => new ReadOnlyCollection<INode<T>>(_searchedNodes.ToList());

        private readonly HashSet<INode<T>> _searchedNodes = new HashSet<INode<T>>();
        
        public BreadthFirstSearch(INode<T> sourceNode)
        {
            _sourceNode = sourceNode;
        }

        public void Execute()
        {
            while (ExecuteOneLevel().MoveNext()) 
            {                
            }
        }
      

        public IEnumerator ExecuteOneLevel()
        {
            Queue<INode<T>> nextNodes = new Queue<INode<T>>(); 
            nextNodes.Enqueue(_sourceNode);            

            do
            {
                INode<T> node = nextNodes.Dequeue();
                
                if (node == null)
                    yield break;
                
                foreach (var edge in node.Edges)
                {
                     if (edge.Start.Equals(node))
                         nextNodes.Enqueue(edge.End);
                }                
                
                _searchedNodes.Add(node);
                
                yield return null;

            } while (true);            
        }
    }
}

