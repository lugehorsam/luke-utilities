using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Linq;

[Serializable]
public struct TriangleDatum {

    public ReadOnlyCollection<VertexDatum> VertexData
    {
        get
        {
            return new ReadOnlyCollection<VertexDatum>(new VertexDatum[]{
                this[0],
                this[1],
                this[2]
            });
        }
    }

    public ReadOnlyCollection<EdgeDatum> EdgeData
    {
        get
        {
            return new ReadOnlyCollection<EdgeDatum>( 
                new EdgeDatum[] {
                    new EdgeDatum(this[0], this[1]),
                    new EdgeDatum(this[1], this[2]),
                    new EdgeDatum(this[2], this[0])
                });
        }
    }

    [SerializeField]
    VertexDatum vertex1;
    [SerializeField]
    VertexDatum vertex2;
    [SerializeField]
    VertexDatum vertex3;

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

    public static Mesh ToMesh(TriangleDatum[] triangles)
    {
        List<int> triangleIndices = new List<int> ();
        List<Vector3> vertices = new List<Vector3>();

        foreach (TriangleDatum triangle in triangles)
        {
            foreach (VertexDatum vertex in triangle.VertexData)
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
            ReadOnlyCollection<VertexDatum> vertexData = currentTriangle.VertexData;
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
            if (Array.IndexOf(otherTriangle.VertexData.ToArray(), this[i], 0) > -1)
            {
                numShared++;
            }
        }
        return numShared;
    }

    public bool HasSharedEdge(TriangleDatum otherTriangle)
    {
        return NumSharedVertices(otherTriangle) > 1;
    }

    public IEnumerable<EdgeDatum> EdgesContaining(VertexDatum vertex)
    {
        return EdgeData.Where((edge) => edge.Vertices.Contains(vertex));
    }

    public TriangleDatum(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3)
    {
        this.vertex1 = vertex1;
        this.vertex2 = vertex2;
        this.vertex3 = vertex3;
    }
}
