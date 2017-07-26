using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Meshes
{
    [Serializable]
    public class SimpleProceduralMesh : IProceduralMesh
    {
        public ReadOnlyCollection<TriangleMesh> TriangleMeshes => new ReadOnlyCollection<TriangleMesh>(_triangles);
        
        protected readonly List<TriangleMesh> _triangles = new List<TriangleMesh>();
        
        protected IEnumerable<Vertex> Vertices => _triangles.SelectMany(triangle => triangle.Vertices);
               
        public Mesh ToUnityMesh()
        {
            return TriangleMesh.ToUnityMesh(_triangles);
        }

        public void AddDepth(float depth)
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
        }

        bool AnyUnsharedVertices(Vertex vertex1, Vertex vertex2)
        {
            return IsUnshared(vertex1) || IsUnshared(vertex2);
        }

        bool IsUnshared(Vertex vertex)
        {
            return _triangles.Count(triangle => triangle.Vertices.Contains(vertex)) == 1;
        }

        public void SetUniqueTriangles()
        {
            foreach (var triangle in _triangles)
            {
                triangle.Vertex1 = new Vertex(triangle.Vertex1.AsVector3);
                triangle.Vertex2 = new Vertex(triangle.Vertex2.AsVector3);
                triangle.Vertex3 = new Vertex(triangle.Vertex3.AsVector3);

            }
        }
    }
}
