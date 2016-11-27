using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections;

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
        TriangleDatum currTriangle = default(TriangleDatum);
        for (int i = 0; i < mesh.triangles.Length; i++)
        {
            Debug.Log("i is " + i);
            if (i % 3 == 0)
            {
                Debug.Log("creating new triangle");
                currTriangle = new TriangleDatum();
                triangles.Add(currTriangle);
            }
            currTriangle[i % 3] = mesh.vertices[mesh.triangles[i]];
        }
        return triangles.ToArray();
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
}
