namespace Mesh
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using UnityEngine;

    using Utilities;

    [Serializable] public class TriangleMesh : SimpleProceduralMesh
    {
        [SerializeField] private Vertex vertex1;
        [SerializeField] private Vertex vertex2;
        [SerializeField] private Vertex vertex3;

        public TriangleMesh(float meshRadius = 1f)
        {
            vertex1 = new Vertex(-meshRadius, -meshRadius, 0);
            vertex2 = new Vertex(0, meshRadius, 0);
            vertex3 = new Vertex(meshRadius, -meshRadius, 0);
            _triangles.Add(this);
        }

        public TriangleMesh(Vertex vertex1, Vertex vertex2, Vertex vertex3)
        {
            this.vertex1 = vertex1;
            this.vertex2 = vertex2;
            this.vertex3 = vertex3;
            _triangles.Add(this);
        }

        public ReadOnlyCollection<Vertex> Vertices
        {
            get { return new ReadOnlyCollection<Vertex>(new[] {this[0], this[1], this[2]}); }
        }

        public Vertex Vertex1
        {
            get { return vertex1; }

            set { vertex1 = value; }
        }

        public Vertex Vertex2
        {
            get { return vertex2; }

            set { vertex2 = value; }
        }

        public Vertex Vertex3
        {
            get { return vertex3; }

            set { vertex3 = value; }
        }

        public Vertex this[int vertexIndex]
        {
            get
            {
                switch (vertexIndex)
                {
                    case 0:
                        return vertex1;
                    case 1:
                        return vertex2;
                    case 2:
                        return vertex3;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (vertexIndex)
                {
                    case 0:
                        vertex1 = value;
                        break;
                    case 1:
                        vertex2 = value;
                        break;
                    case 2:
                        vertex3 = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        public static TriangleMesh[] FromMesh(Mesh unityMesh)
        {
            var triangles = new List<TriangleMesh>();
            var currTriangle = new TriangleMesh();
            for (var i = 0; i < unityMesh.vertices.Length; i++)
            {
                Vector3 currVertex = unityMesh.vertices[unityMesh.triangles[i]];
                currTriangle[i % 3] = new Vertex(currVertex);
                if ((i + 1) % 3 == 0)
                {
                    triangles.Add(currTriangle);
                    currTriangle = new TriangleMesh();
                }
            }

            return triangles.ToArray();
        }

        public static Mesh ToUnityMesh(IEnumerable<TriangleMesh> triangles)
        {
            var triangleIndices = new List<int>();
            var vertices = new List<Vector3>();

            foreach (TriangleMesh triangle in triangles)
            {
                foreach (Vertex vertex in triangle.Vertices)
                {
                    int vertexIndex = vertices.IndexOf(vertex.AsVector3);

                    if (vertexIndex < 0)
                    {
                        vertexIndex = vertices.Count;
                        vertices.Add(vertex.AsVector3);
                    }

                    triangleIndices.Add(vertexIndex);
                }
            }

            var newUnityMesh = new Mesh();
            newUnityMesh.vertices = vertices.ToArray();
            newUnityMesh.triangles = triangleIndices.ToArray();
            return newUnityMesh;
        }

        public static Dictionary<Vertex, List<TriangleMesh>> GetVertexTriangleMap(TriangleMesh[] triangles)
        {
            var map = new Dictionary<Vertex, List<TriangleMesh>>();
            for (var i = 0; i < triangles.Length; i++)
            {
                TriangleMesh currentTriangle = triangles[i];
                ReadOnlyCollection<Vertex> vertexData = currentTriangle.Vertices;
                for (var j = 0; j < vertexData.Count; i++)
                {
                    Vertex datum = vertexData[j];
                    if (!map.ContainsKey(datum))
                    {
                        map[datum] = new List<TriangleMesh>();
                    }

                    map[datum].Add(currentTriangle);
                }
            }

            return map;
        }

        public int NumSharedVertices(TriangleMesh otherTriangle)
        {
            var numShared = 0;
            for (var i = 0; i < 3; i++)
            {
                if (Array.IndexOf(otherTriangle.Vertices.ToArray(), this[i], 0) > -1)
                {
                    numShared++;
                }
            }

            return numShared;
        }

        public void SortVertices()
        {
            Vertex[] newVertices = Vertices.ToArray();

            Array.Sort(newVertices, this);
            vertex1 = newVertices[0];
            vertex2 = newVertices[1];
            vertex3 = newVertices[2];
        }

        public bool HasSharedEdge(TriangleMesh otherTriangle)
        {
            return NumSharedVertices(otherTriangle) > 1;
        }

        public override string ToString()
        {
            return Vertices.PrettyPrint();
        }

        public TriangleMesh CreateCopy(Func<Vertex, Vertex> vertexProcessor)
        {
            return new TriangleMesh(vertexProcessor(vertex1), vertexProcessor(vertex2), vertexProcessor(vertex3));
        }
    }
}
