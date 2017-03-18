/**
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent (typeof (MeshFilter))]

public class DynamicMesh : DatumBehavior<MeshDatum> {

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    MeshCollider meshCollider;

    void Awake()
    {
        meshFilter = GetComponent<MeshFilter> ();
        meshRenderer = GetComponent<MeshRenderer> ();
        meshCollider = GetComponent<MeshCollider>();
    }

    protected override void HandleDataUpdate(MeshDatum oldData, MeshDatum newData)
    {
        if (meshFilter.mesh != null)
        {
            meshFilter.mesh.Clear();
        }
        meshFilter.mesh = newData.Mesh;
        meshRenderer.material.color = newData.Color;
        meshCollider.sharedMesh = newData.Mesh;
        meshFilter.mesh.RecalculateBounds();
        meshFilter.mesh.RecalculateNormals();

    }
}

**/
