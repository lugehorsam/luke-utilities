using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[RequireComponent(typeof(TouchDispatcher))]
public class SliceOnDrag : GameBehavior {

    TouchDispatcher touchDispatcher;
    new Collider collider;

    [SerializeField]
    bool debugMode;

    protected override void InitComponents()
    {
        touchDispatcher = GetComponent<TouchDispatcher>();
        collider = GetComponent<Collider>();
    }

    protected sealed override void AddEventHandlers()
    {
        touchDispatcher.OnRelease += OnDragEnd;
    }

    protected sealed override void RemoveEventHandlers()
    {
        touchDispatcher.OnRelease -= OnDragEnd;
    }

    void OnDragEnd(TouchDispatcher dispatcher, Gesture currentDrag) {

        Gesture[] collisionGestures = currentDrag.Filter((frame) =>
        {
            RaycastHit? hit = frame.HitForCollider(collider);
            return hit.HasValue && hit.Value.collider == collider;
        });

        List<Gesture> sliceGestures = new List<Gesture>();
        for (int gestureIndex = 0; gestureIndex < collisionGestures.Length; gestureIndex++)
        {
            if (debugMode)
            {
                Debug.DrawLine(collisionGestures[gestureIndex].MousePositionStart, (collisionGestures[gestureIndex].MousePositionCurrent));
            }
        }
    }
}
