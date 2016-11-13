using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class LineBinding : PropertyBinding<Vector3, LineRenderer> {

    [SerializeField]
    Vector3[] linePositions;

    protected override void InitComponents()
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
