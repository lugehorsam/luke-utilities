using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MeshAsset : ScriptableObject {

    [SerializeField]
    private TriangleDatum[] triangles;

    [SerializeField]
    private MeshAsset[] meshAssets;

    public Mesh ToMesh()
    {
        List<TriangleDatum> effectiveTriangles = new List<TriangleDatum>(triangles);       
        effectiveTriangles.AddRange(meshAssets.SelectMany(mesh => mesh.triangles));            
        return TriangleDatum.ToMesh(triangles);
    }
}
