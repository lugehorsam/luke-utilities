namespace Utilities
{
    using System.Collections.Generic;
    using System.Linq;

    public class DepthFirstSearch
    {
        public HashSet<T> Search<T>(Graph<T> graph, T start)
        {
            var visited = new HashSet<T>();

            if (!graph.Nodes.Contains(start))
            {
                return visited;
            }

            var stack = new Stack<T>();
            stack.Push(start);

            while (stack.Count > 0)
            {
                T vertex = stack.Pop();

                if (visited.Contains(vertex))
                {
                    continue;
                }

                visited.Add(vertex);

                foreach (T neighbor in graph[vertex])
                {
                    if (!visited.Contains(neighbor))
                    {
                        stack.Push(neighbor);
                    }
                }
            }

            return visited;
        }
    }
}
