using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRendererExtensions))]
[RequireComponent(typeof(MeshFilter))]
public class ColorMesh : MonoBehaviour {

    [SerializeField]
    Color color;
    [SerializeField]
    MeshRenderer meshRenderer;
    void Awake() {
        meshRenderer.material.color = color;       
    }
}
