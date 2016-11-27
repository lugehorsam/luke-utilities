using UnityEngine;
using System.Collections.Generic;

public struct MeshDatum {

    TriangleDatum[] triangles;

    public static MeshDatum FromMesh(Mesh mesh)
    {
        MeshDatum meshDatum = new MeshDatum();
        meshDatum.triangles = TriangleDatum.FromMesh(mesh);
        return meshDatum;
    }
}
