namespace Utilities
{
    using System;
    using System.Collections.Generic;

    public class Cycle<T> where T : class
    {

        public static Cycle<T> Create(Graph<T> graph, List<T> vertices)
        {
            throw new NotImplementedException(); /**
            if (!IsValid(graph, vertices))
            {
                throw new Exception($"Tried to create cycle from graph {graph} using invalid vertices {vertices}");
            }

            var newCycle = new Cycle<T>();

            return newCycle;**/
        }

        public static bool IsValid(Graph<T> graph, List<T> vertices)
        {
            var asLinkedList = new LinkedList<T>(vertices);

            LinkedListNode<T> currentVertex = null;

            while (currentVertex != asLinkedList.Last)
            {
                throw new NotImplementedException();
            }

            foreach (T vertex in asLinkedList)
            {
                bool isLastVertex = vertex == asLinkedList.Last.Value;

                T secondVertex;

                if (isLastVertex)
                {
                    secondVertex = asLinkedList.First.Value;
                }
                else
                {
                    throw new NotImplementedException();
                }

                if (!graph.IsEdgeBetween(vertex, secondVertex))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
