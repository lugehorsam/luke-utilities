namespace Utilities
{
    using System.Collections.Generic;
    
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
                        
            foreach (var node in allNodes)
            {
                HashSet<T> searchedNodes;
                
                if (IsBackEdge(node, out searchedNodes))
                {
                    cycles.Add(searchedNodes);
                }
            }
            
            return cycles;
        }

        private bool IsBackEdge(T node, out HashSet<T> nodes)
        {
            nodes = null;
            
            var dfs = new DepthFirstSearch<T>(_graph, node, true);
            
            dfs.Run();
            
            if (dfs.BrokeOnCycle)
            {
                nodes = new HashSet<T>(dfs.CurrentComponent);
                return true;
            }

            return false;
        }
    }
}
