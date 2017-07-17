/**using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Utilities.Meshes
{
    public class MeshEdge
    {
        private const float POINT_ON_EDGE_TOLERANCE = .01f;
    
        public ReadOnlyCollection<Vertex> Vertices
        {
            get
            {
                return new ReadOnlyCollection<Vertex>(new []{Vertex1, Vertex2});
            }
        }
    
        public Vertex Vertex1
        {
            get;
            private set;
        }
    
        public Vertex Vertex2
        {
            get;
            private set;
        }
    
        public float MinY
        {
            get
            {
                return Mathf.Min(Vertex1.Y, Vertex2.Y);
            }
        }
    
        public float MaxY
        {
            get
            {
                return Mathf.Max(Vertex1.Y, Vertex2.Y);
            }
        }
    
        public float MinX
        {
            get
            {
                return Mathf.Min(Vertex1.X, Vertex2.X);
            }
        }
    
        public float MaxX
        {
            get
            {
                return Mathf.Max(Vertex1.X, Vertex2.X);
            }
        }
    
        public bool VertexLiesOnEdge(Vertex vertex)
        {
            Vector3 combinedMidpointDist = (Vertex1 - vertex) + (vertex - Vertex2);
            Vector3 endpointDist = (Vertex1 - Vertex2);
            bool vertexWithinEdgeRect = VertexWithinEdgeRect(vertex);
            bool liesOnEdge = combinedMidpointDist == endpointDist && vertexWithinEdgeRect;
            return liesOnEdge;
        }
    
        public bool VertexWithinEdgeRect(Vertex vertex)
        {
            return MinX.ApproximatelyLessThan(vertex.X, POINT_ON_EDGE_TOLERANCE) &&
                   vertex.X.ApproximatelyLessThan(MaxX, POINT_ON_EDGE_TOLERANCE) &&
                   MinY.ApproximatelyLessThan(vertex.Y, POINT_ON_EDGE_TOLERANCE) &&
                   vertex.Y.ApproximatelyLessThan(MaxY, POINT_ON_EDGE_TOLERANCE);
        }
    
        public MeshEdge(Vertex vertex1, Vertex vertex2)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
        }
    
        public MeshEdge(IList<Vector3> vectorList)
        {
            Vertex1 = vectorList.First();
            Vertex2 = vectorList.Last();
        }
    
        /// <summary>
        /// </summary>
        /// <param name="otherMeshEdge"></param>
        /// <param name="connectionMargin"></param>
        /// <returns></returns>
        public bool HasIntersectionWithEdge(MeshEdge otherMeshEdge, bool onThisEdge = true, bool onOtherEdge = true)
        {
            Vertex? intersectionPoint = GetIntersectionWithEdge(otherMeshEdge, onThisEdge, onOtherEdge);
            return intersectionPoint.HasValue;
        }
    
        public bool ConnectsToEdge(MeshEdge otherMeshEdge, float acceptableDifference = 0f)
        {
            MeshEdge thisMeshEdge = this;
            bool isConnection = otherMeshEdge.Vertices.Any(
                (otherVertex) => otherVertex == thisMeshEdge.Vertex1 || otherVertex == thisMeshEdge.Vertex2);
            return isConnection;
        }
    
        /// <summary>
        /// See https://www.topcoder.com/community/array-science/array-science-tutorials/geometry-concepts-line-intersection-and-its-applications
        /// Returns false if edges are connected at the point of intersection.
        /// </summary>
        /// <param name="otherMeshEdge"></param>
        /// <param name="connectionMargin">Any vertices whose distance from one another
        /// fall under the connection margin will not belong to edges that can intersect one another.
        /// </param>
        /// <returns></returns>
        public Vector3? GetIntersectionWithEdge(MeshEdge otherMeshEdge, bool onThisEdge = true, bool onOtherEdge = true)
        {
            if (otherMeshEdge.ConnectsToEdge(this))
            {
                return null;
            }
    
            float thisYDiff = Vertex2.Y - Vertex1.Y;
            float thisXDiff = Vertex1.X - Vertex2.X;
            double thisC = thisYDiff * Vertex1.X + thisXDiff * Vertex1.Y;
    
            float otherYDiff = otherMeshEdge.Vertex2.Y - otherMeshEdge.Vertex1.Y;
            float otherXDiff = otherMeshEdge.Vertex1.X - otherMeshEdge.Vertex2.X;
            double otherC = otherYDiff * otherMeshEdge.Vertex1.X + otherXDiff * otherMeshEdge.Vertex1.Y;
    
            double det = thisYDiff * otherXDiff - otherYDiff * thisXDiff;
    
            if (det != 0f)
            {
                double x = (otherXDiff * thisC - thisXDiff * otherC) / det;
                double y = (thisYDiff * otherC - otherYDiff * thisC) / det;
                Vector3 intersectionPoint = new Vector3((float) x, (float) y, 0f);
                if ((!onThisEdge || VertexLiesOnEdge(intersectionPoint)) &&
                    (!onOtherEdge || otherMeshEdge.VertexLiesOnEdge(intersectionPoint)))
                {
                    return intersectionPoint;
                }
            }
    
            return null;
        }
    
        public override string ToString()
        {
            return string.Format("[EdgeDatum: Vertex1={0}, Vertex2={1}, MinY={2}, MaxY={3}, MinX={4}, MaxX={5}]", Vertex1, Vertex2, MinY, MaxY, MinX, MaxX);
        }
        
        from triangle:
        public IEnumerable<MeshEdge> EdgesContaining(Vertex vertex)
        {
            return EdgeData.Where((edge) => edge.Vertices.Contains(vertex));
        }
        
        from triangle:
          public bool HasEdgeThatIntersects(MeshEdge otherMeshEdge, bool onThisEdge = true, bool onOtherEdge = true)
        {
            return EdgeData.Any((edge) => edge.HasIntersectionWithEdge(otherMeshEdge, onThisEdge, onOtherEdge));
        }
    }
}
**/