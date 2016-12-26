using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent (typeof (MeshFilter))]

public class DynamicMesh : DatumBehavior<DynamicMeshDatum> {

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    MeshCollider meshCollider;

    protected override void InitComponents()
    {
        meshFilter = GetComponent<MeshFilter> ();
        meshRenderer = GetComponent<MeshRenderer> ();
        meshCollider = GetComponent<MeshCollider>();
    }

    protected override void HandleDataUpdate(DynamicMeshDatum oldData, DynamicMeshDatum newData)
    {
        meshFilter.mesh = newData.Mesh;
        meshFilter.mesh.RecalculateBounds();
        meshRenderer.material.color = newData.Color;
        meshCollider.sharedMesh = newData.Mesh;
    }
}
