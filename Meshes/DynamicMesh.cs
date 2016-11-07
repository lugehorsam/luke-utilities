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

    void Awake ()
    {
        meshFilter = GetComponent<MeshFilter> ();
        meshRenderer = GetComponent<MeshRenderer> ();
    }

	public void Rebuild () {
        Mesh mesh = meshFilter.mesh;
        mesh.Clear ();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        meshRenderer.material.color = color;
    }
}
