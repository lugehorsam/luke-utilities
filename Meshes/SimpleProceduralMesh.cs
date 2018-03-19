namespace Mesh
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using UnityEngine;

    using Utilities;

    [Serializable] public class SimpleProceduralMesh : IProceduralMesh, IComparer<Vertex>
    {
        protected readonly List<TriangleMesh> _triangles = new List<TriangleMesh>();

        public int Compare(Vertex vertex1, Vertex vertex2)
        {
            if (vertex1 == vertex2)
            {
                return 0;
            }

            Vector3 anchorVertex = GetCenterPoint();
            Vector3 vector1 = vertex1.AsVector3 - anchorVertex;
            Vector3 vector2 = vertex2.AsVector3 - anchorVertex;
            float angle = MathExt.GetSignedAngle(vector1, vector2);
            int sign = Math.Sign(angle);
            return sign;
        }

        public ReadOnlyCollection<TriangleMesh> TriangleMeshes
        {
            get { return new ReadOnlyCollection<TriangleMesh>(_triangles); }
        }

        public Mesh ToUnityMesh()
        {
            return TriangleMesh.ToUnityMesh(_triangles);
        }

        /**public void AddDepth(float depth)
        {
            var endCopy = new List<TriangleMesh>();
            var connections = new List<SquareMesh>();
            
            for (int i = 0; i < _triangles.Count; i++)
            {
                var topTriangle = _triangles[i];
                
                var bottomTriangle = topTriangle.CreateCopy
                (
                    vertex => new Vertex(vertex.X, vertex.Y, depth)
                );
                
                endCopy.Add
                (
                    bottomTriangle    
                );

                var unculledFaces = new List<Tuple<int, int>>();

                foreach (var edge in topTriangle.AsEdges)
                {
                    var vert1 = topTriangle.Vertices[edge.Start.Value];
                    var vert2 = topTriangle.Vertices[edge.End.Value];

                    if (AnyUnsharedVertices(vert1, vert2))
                    {
                        unculledFaces.Add(Tuple.Create(edge.Start.Value, edge.End.Value));
                    }
                }

                foreach (var unculledFace in unculledFaces)
                {
                    connections.Add
                    (
                        new SquareMesh
                        (
                            topTriangle.Vertices[unculledFace.Item1],
                            topTriangle.Vertices[unculledFace.Item2],
                            bottomTriangle.Vertices[unculledFace.Item1], 
                            bottomTriangle.Vertices[unculledFace.Item2]
                        )  
                    );
                }                          
            }
            
            _triangles.AddRange(endCopy);            
            _triangles.AddRange(connections.SelectMany(connection => connection._triangles));
        }**/

        private bool AnyUnsharedVertices(Vertex vertex1, Vertex vertex2)
        {
            return IsUnshared(vertex1) || IsUnshared(vertex2);
        }

        private bool IsUnshared(Vertex vertex)
        {
            return _triangles.Count(triangle => triangle.Vertices.Contains(vertex)) == 1;
        }

        public void SetUniqueTriangles()
        {
            foreach (TriangleMesh triangle in _triangles)
            {
                triangle.Vertex1 = new Vertex(triangle.Vertex1.AsVector3);
                triangle.Vertex2 = new Vertex(triangle.Vertex2.AsVector3);
                triangle.Vertex3 = new Vertex(triangle.Vertex3.AsVector3);
            }
        }

        protected IEnumerable<Vertex> GetVertices()
        {
            var vertices = new HashSet<Vertex>();

            foreach (Vertex vertex in _triangles.SelectMany(triangle => triangle.Vertices))
            {
                vertices.Add(vertex);
            }

            return vertices;
        }

        public Vector3 GetCenterPoint()
        {
            IEnumerable<Vertex> vertices = GetVertices();
            int count = vertices.Count();

            float totalX = 0;
            float totalY = 0;
            float totalZ = 0;

            foreach (Vertex vertex in vertices)
            {
                totalX += vertex.X;
                totalY += vertex.Y;
                totalZ += vertex.Z;
            }

            return new Vector3(totalX / count, totalY / count, totalZ / count);
        }

        protected Vertex[] GetSortedVertices()
        {
            Vertex[] verts = GetVertices().ToArray();
            Array.Sort(verts, this);
            return verts;
        }
    }
}
