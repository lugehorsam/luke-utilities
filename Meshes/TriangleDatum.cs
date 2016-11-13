using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public struct TriangleDatum {

    [SerializeField]
    VertexDatum[] vertexData;

     public static TriangleDatum[] FromMesh(Mesh mesh)
     {
        List<TriangleDatum> triangles = new List<TriangleDatum>();
        TriangleDatum currTriangle = new TriangleDatum();
        for (int i = 0; i < mesh.triangles.Length; i++)
        {
            if (i % 2 == 0)
            {
                currTriangle = new TriangleDatum();
            }
            currTriangle.vertexData[i % 2] = mesh.vertices[mesh.triangles[i]];
        }
        return triangles.ToArray();
    }

    public int NumSharedVertices(TriangleDatum otherTriangle)
    {
        int numShared = 0;
        for (int i = 0; i < vertexData.Length; i++)
        {
            if (Array.IndexOf(otherTriangle.vertexData, vertexData[i], 0) > -1)
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
