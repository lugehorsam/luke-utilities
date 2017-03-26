using System;
using System.Collections.Generic;
using UnityEngine;

public class LineBinding : Vector3Binding<LineRenderer>
{

    private readonly List<Vector3> linePositions;
    
    public LineBinding(List<Vector3> initialLinePositions,
        MonoBehaviour coroutineRunner, GameObject gameObject
    ) : base(coroutineRunner, gameObject)
    {
        Component.SetVertexCount(initialLinePositions.Count);
    }
    
    public override void SetProperty(Vector3 position) {
        linePositions [linePositions.Count - 1] = position;
        Component.SetPositions (linePositions.ToArray());
    }

    public override Vector3 GetProperty() {
        return linePositions [linePositions.Count - 1];
    }
}
