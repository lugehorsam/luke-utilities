using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Utilities
{

    [Serializable]
    public struct TriangleDatum : IComparer<VertexDatum>
    {

        public CycleDirection CycleDirection
        {
            get { return cycleDirection; }
            set { cycleDirection = value; }
        }

        private CycleDirection cycleDirection;

        public ReadOnlyCollection<VertexDatum> Vertices
        {
            get
            {
                return new ReadOnlyCollection<VertexDatum>(new VertexDatum[]
                {
                    this[0],
                    this[1],
                    this[2]
                });
            }
        }

        public ReadOnlyCollection<MeshEdge> EdgeData
        {
            get
            {
                return new ReadOnlyCollection<MeshEdge>(
                    new MeshEdge[]
                    {
                        new MeshEdge(this[0], this[1]),
                        new MeshEdge(this[1], this[2]),
                        new MeshEdge(this[2], this[0])
                    });
            }
        }

        [SerializeField] VertexDatum vertex1;
        [SerializeField] VertexDatum vertex2;
        [SerializeField] VertexDatum vertex3;

        public VertexDatum this[int vertexIndex]
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

        public static TriangleDatum[] FromMesh(Mesh mesh)
        {
            List<TriangleDatum> triangles = new List<TriangleDatum>();
            TriangleDatum currTriangle = new TriangleDatum();
            for (int i = 0; i < mesh.triangles.Length; i++)
            {

                Vector3 currVertex = mesh.vertices[mesh.triangles[i]];
                currTriangle[i % 3] = currVertex;
                if ((i + 1) % 3 == 0)
                {
                    triangles.Add(currTriangle);
                    currTriangle = new TriangleDatum();
                }
            }
            return triangles.ToArray();
        }

        public static Mesh ToMesh(ICollection<TriangleDatum> triangles)
        {
            List<int> triangleIndices = new List<int>();
            List<Vector3> vertices = new List<Vector3>();

            foreach (TriangleDatum triangle in triangles)
            {
                foreach (VertexDatum vertex in triangle.Vertices)
                {
                    int vertexIndex = vertices.IndexOf(vertex);

                    if (vertexIndex < 0)
                    {
                        vertexIndex = vertices.Count;
                        vertices.Add(vertex);
                    }

                    triangleIndices.Add(vertexIndex);
                }
            }

            Mesh newMesh = new Mesh();
            newMesh.vertices = vertices.ToArray();
            newMesh.triangles = triangleIndices.ToArray();
            return newMesh;
        }

        public static Dictionary<VertexDatum, List<TriangleDatum>> GetVertexTriangleMap(TriangleDatum[] triangles)
        {

            Dictionary<VertexDatum, List<TriangleDatum>> map = new Dictionary<VertexDatum, List<TriangleDatum>>();
            for (int i = 0; i < triangles.Length; i++)
            {
                TriangleDatum currentTriangle = triangles[i];
                ReadOnlyCollection<VertexDatum> vertexData = currentTriangle.Vertices;
                for (int j = 0; j < vertexData.Count; i++)
                {
                    VertexDatum datum = vertexData[j];
                    if (!map.ContainsKey(datum))
                    {
                        map[datum] = new List<TriangleDatum>();
                    }
                    map[datum].Add(currentTriangle);
                }
            }
            return map;
        }

        public int NumSharedVertices(TriangleDatum otherTriangle)
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

        public bool HasEdgeThatIntersects(MeshEdge otherMeshEdge, bool onThisEdge = true, bool onOtherEdge = true)
        {
            return EdgeData.Any((edge) => edge.HasIntersectionWithEdge(otherMeshEdge, onThisEdge, onOtherEdge));
        }

        public int Compare(VertexDatum vertex1, VertexDatum vertex2)
        {
            if (vertex1 == vertex2)
            {
                return 0;
            }

            VertexDatum anchorVertex = GetCenterPoint();
            Vector3 vector1 = vertex1 - anchorVertex;
            Vector3 vector2 = vertex2 - anchorVertex;
            float angle = MathUtils.GetSignedAngle(vector1, vector2);
            int sign = Math.Sign(angle);
            return cycleDirection == CycleDirection.Clockwise ? sign : -sign;
        }

        public void SortVertices()
        {
            VertexDatum[] newVertices = Vertices.ToArray();

            Array.Sort(newVertices, this);
            vertex1 = newVertices[0];
            vertex2 = newVertices[1];
            vertex3 = newVertices[2];
        }

        public bool HasSharedEdge(TriangleDatum otherTriangle)
        {
            return NumSharedVertices(otherTriangle) > 1;
        }

        public IEnumerable<MeshEdge> EdgesContaining(VertexDatum vertex)
        {
            return EdgeData.Where((edge) => edge.Vertices.Contains(vertex));
        }

        public TriangleDatum(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3)
        {
            this.vertex1 = vertex1;
            this.vertex2 = vertex2;
            this.vertex3 = vertex3;
            cycleDirection = CycleDirection.Clockwise;
        }

        public override string ToString()
        {
            return Vertices.ToFormattedString();
        }
    }
}