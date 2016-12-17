using UnityEngine;
using System.Collections;

public class MeshAsset : ScriptableObject {

    [SerializeField]
    TriangleDatum[] triangles;

    public Mesh ToMesh()
    {
        return TriangleDatum.ToMesh(triangles);
    }
}
