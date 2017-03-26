using Utilities;
using System.Collections.Generic;
using UnityEngine;

public class LineBinding : Vector3Binding<LineRenderer>
{
    public ObservableList<Vector3> LinePositions
    {
        get { return linePositions; }
    }
    
    private readonly ObservableList<Vector3> linePositions = new ObservableList<Vector3>();
    
    public LineBinding(
        MonoBehaviour coroutineRunner, GameObject gameObject
    ) : base(coroutineRunner, gameObject)
    {
        linePositions.OnAdd += HandlePositionAdd;
        linePositions.OnRemove += HandlePositionRemove;
    }    
    
    public override void SetProperty(Vector3 position)
    {
        linePositions [linePositions.Count - 1] = position;
    }

    public override Vector3 GetProperty() 
    {
        return linePositions [linePositions.Count - 1];
    }

    void HandlePositionAdd(Vector3 point)
    {
        Component.SetPositions (linePositions.ToArray());   
    }

    void HandlePositionRemove(Vector3 point, int removalIndex)
    {
        Component.SetPositions (linePositions.ToArray());   
    }
}
