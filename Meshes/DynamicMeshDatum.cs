using System;
using UnityEngine;

[Serializable]
public struct DynamicMeshDatum
{
    public Mesh Mesh
    {
        get { return useMeshAsset ? meshAsset.ToMesh() : TriangleDatum.ToMesh(triangles); }
    }

    [SerializeField]
    private TriangleDatum[] triangles;

    public Color Color
    {
        get { return color; }
    }

    [SerializeField]
    private Color color;

    [SerializeField] private MeshAsset meshAsset;
    [SerializeField] private bool useMeshAsset;

    public DynamicMeshDatum(TriangleDatum[] triangles, Color color, MeshAsset meshAsset = null, bool useMeshAsset = false)
    {
        this.triangles = triangles;
        this.color = color;
        this.meshAsset = meshAsset;
        this.useMeshAsset = useMeshAsset;
    }
}
