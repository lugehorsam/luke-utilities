using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent (typeof (MeshFilter))]

public class DynamicMesh : MonoBehaviour {

    [SerializeField]
    Vector3 [] vertices;
    [SerializeField]
    int [] triangles;

    [SerializeField]
    Color color;

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;

    void Awake ()
    {
        meshFilter = GetComponent<MeshFilter> ();
        meshRenderer = GetComponent<MeshRenderer> ();
    }

	void Start () {
        Mesh mesh = meshFilter.mesh;
        mesh.Clear ();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        meshRenderer.material.color = color;
    }
}
