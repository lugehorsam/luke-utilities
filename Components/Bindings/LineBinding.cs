using System;
using UnityEngine;

public class LineBinding : Vector3Binding<LineRenderer> {

    [SerializeField]
    Vector3[] linePositions;

    public LineBinding(MonoBehaviour coroutineRunner, GameObject gameObject) : base(coroutineRunner, gameObject)
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
