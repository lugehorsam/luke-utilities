namespace Utilities
{
    using System.Collections.Generic;
    using System.Linq;

    public class CycleSearch<T>
    {
        private readonly Graph<T> _graph;

        public CycleSearch(Graph<T> graph)
        {
            _graph = graph;
        }
        
        public IEnumerable<IEnumerable<T>> GetCycles()
        {
            var cycles = new List<IEnumerable<T>>();
            
            IEnumerable<T> allNodes = _graph.Nodes;
                        
            HashSet<T> allSearchedNodes = new HashSet<T>();

            foreach (var node in allNodes)
            {
                if (allSearchedNodes.Contains(node))
                {
                    continue;
                }
                
                HashSet<T> searchedNodes;

                if (IsBackEdge(node, out searchedNodes))
                {
                    cycles.Add(searchedNodes);
                }
                
                foreach (var searchedNode in searchedNodes)
                {
                    allSearchedNodes.Add(searchedNode);
                }                
            }
            
            return cycles;
        }

        private bool IsBackEdge(T node, out HashSet<T> nodes)
        {
            nodes = null;
            
            var dfs = new DepthFirstSearch<T>(_graph, node, true);
            
            dfs.Run();
            
            nodes = new HashSet<T>(dfs.CurrentComponent);

            return dfs.BrokeOnCycle;
        }
    }
}
