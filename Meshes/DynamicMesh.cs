using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent (typeof (MeshFilter))]

public class DynamicMesh : MonoBehaviour {

    public Vector3[] Vertices
    {
        get
        {
            return vertices;
        }
    }
    public int[] Triangles
    {
        get
        {
            return triangles;
        }
    }

    [SerializeField]
    Vector3 [] vertices;
    [SerializeField]
    int [] triangles;

    [SerializeField]
    Color color;

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    MeshCollider meshCollider;

    [SerializeField]
    bool rebuildOnAwake = true;

    [SerializeField]
    bool useMeshAsset;

    [SerializeField]
    MeshAsset meshAsset;

    void Awake ()
    {
        meshFilter = GetComponent<MeshFilter> ();
        meshRenderer = GetComponent<MeshRenderer> ();
        meshCollider = GetComponent<MeshCollider>();

        if (rebuildOnAwake)
        {
            Rebuild();
        }
    }

    public void Rebuild()
    {

        Mesh mesh;
        if (useMeshAsset)
        {
            mesh = meshAsset.ToMesh();
            Debug.Log("Mesh is " + mesh);
        }
        else
        {
            mesh = meshFilter.mesh;
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
        }

        meshFilter.mesh = mesh;
        meshRenderer.material.color = color;
      
        if (meshCollider != null)
        {
            DestroyImmediate(meshCollider);
            meshCollider = gameObject.AddComponent<MeshCollider>();
        }

        meshCollider.sharedMesh = mesh;
    }
}
