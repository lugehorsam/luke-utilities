using UnityEngine;
using System;

[Serializable]
public struct SquareDatum {

    TriangleDatum[] triangles;

    public SquareDatum(TriangleDatum[] triangles)
    {
        if (triangles.Length != 2 || !triangles[0].HasSharedEdge(triangles[1]))
        {
            throw new DataMisalignedException();
        }
        this.triangles = triangles;
    }
}
