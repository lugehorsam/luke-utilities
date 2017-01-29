using System;
using UnityEngine;

public class LineBinding : PropertyBinding<Vector3, LineRenderer> {

    [SerializeField]
    Vector3[] linePositions;

    void Awake()
    {
        Component.SetVertexCount(linePositions.Length);
    }

    public override void SetProperty(Vector3 position) {
        linePositions [linePositions.Length - 1] = position;
        Vector3[] linePoints = Array.ConvertAll (linePositions, (vector3) => vector3);
        Component.SetPositions (linePoints);
    }

    public override Vector3 GetProperty() {
        return linePositions [linePositions.Length - 1];
    }
}
