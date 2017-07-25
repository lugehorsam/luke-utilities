using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Utilities.Meshes
{
    [Serializable]
    public class TriangleMesh : SimpleMesh, IComparer<Vertex>
    {
        public CycleDirection CycleDirection
        {
            get { return cycleDirection; }
            set { cycleDirection = value; }
        }

        private CycleDirection cycleDirection;

        public ReadOnlyCollection<Vertex> Vertices => new ReadOnlyCollection<Vertex>(new []
        {
            this[0],
            this[1],
            this[2]
        });

        public DirectedEdge<int>[] AsEdges => new[]
        {
            new DirectedEdge<int>(0, 1),
            new DirectedEdge<int>(1, 2),
            new DirectedEdge<int>(0, 2),
        };

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

        [SerializeField] Vertex vertex1;
        [SerializeField] Vertex vertex2;
        [SerializeField] Vertex vertex3;

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
            List<TriangleMesh> triangles = new List<TriangleMesh>();
            TriangleMesh currTriangle = new TriangleMesh();
            for (int i = 0; i < unityMesh.vertices.Length; i++)
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
            List<int> triangleIndices = new List<int>();
            List<Vector3> vertices = new List<Vector3>();

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

            Mesh newUnityMesh = new Mesh();
            newUnityMesh.vertices = vertices.ToArray();
            newUnityMesh.triangles = triangleIndices.ToArray();
            return newUnityMesh;
        }

        public static Dictionary<Vertex, List<TriangleMesh>> GetVertexTriangleMap(TriangleMesh[] triangles)
        {

            Dictionary<Vertex, List<TriangleMesh>> map = new Dictionary<Vertex, List<TriangleMesh>>();
            for (int i = 0; i < triangles.Length; i++)
            {
                TriangleMesh currentTriangle = triangles[i];
                ReadOnlyCollection<Vertex> vertexData = currentTriangle.Vertices;
                for (int j = 0; j < vertexData.Count; i++)
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
            int numShared = 0;
            for (int i = 0; i < 3; i++)
            {
                if (Array.IndexOf(otherTriangle.Vertices.ToArray(), this[i], 0) > -1)
                {
                    numShared++;
                }
            }
            return numShared;
        }

        public Vector3 GetCenterPoint()
        {
            return new Vector3(
                (vertex1.X + vertex2.X + vertex3.X) / 3f,
                (vertex1.Y + vertex2.Y + vertex3.Y) / 3f,
                (vertex1.Z + vertex2.Z + vertex3.Z) / 3f
            );
        }      

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
            return cycleDirection == CycleDirection.Clockwise ? sign : -sign;
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
            cycleDirection = CycleDirection.Clockwise;
            _triangles.Add(this);
        }

        public override string ToString()
        {
            return Vertices.ToFormattedString();
        }

        public TriangleMesh CreateCopy(Func<Vertex, Vertex> vertexProcessor)
        {
            return new TriangleMesh
            (
                vertexProcessor(vertex1),
                vertexProcessor(vertex2),
                vertexProcessor(vertex3)
            );
        }
    }
}
