using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Draggable))]
public class SculptableMesh : DynamicMesh {

    Draggable draggable;

    void Awake()
    {
        draggable = GetComponent<Draggable>();
    }
}
