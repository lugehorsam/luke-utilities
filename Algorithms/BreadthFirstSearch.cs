using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Utilities
{
    public class BreadthFirstSearch<T> where T : INode<T>
    {
        private readonly T _sourceNode;
        
        public ReadOnlyCollection<T> SearchedNodes
        {
            get { return new ReadOnlyCollection<T>(_searchedNodes.ToList()); }
        }

        private readonly HashSet<T> _searchedNodes = new HashSet<T>();
        
        public BreadthFirstSearch(T sourceNode)
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
            Queue<T> nextNodes = new Queue<T>(); 
            nextNodes.Enqueue(_sourceNode);            

            do
            {
                T nextNode = nextNodes.Dequeue();      
                
                if (nextNode == null)
                    yield break;
                
                foreach (T targetNode in nextNode.TargetNodes)
                {
                     nextNodes.Enqueue(targetNode);
                }                
                
                _searchedNodes.Add(nextNode);
                
                yield return null;

            } while (true);            
        }
    }
}

