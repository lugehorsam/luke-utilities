using UnityEngine;

[RequireComponent(typeof(Draggable))]
[RequireComponent(typeof(RotationBinding))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class RotateOnDrag : GameBehavior {
    
    Draggable draggable;
    Rigidbody rigidBody;
    Axis dominantAxis;
    RaycastHit dragHitInfo;

    [SerializeField]
    float rotationUnitsPerSwipePixel;

    protected sealed override void AddEventHandlers()
    {
        draggable.OnDrag += OnDrag;
        draggable.OnSelect += OnSelect;
        draggable.OnDragDeselect += OnDragDeselect;
    }

    protected sealed override void RemoveEventHandlers()
    {
        draggable.OnDrag -= OnDrag;
        draggable.OnSelect -= OnSelect;
        draggable.OnDragDeselect -= OnDragDeselect;
    }

    protected override void InitComponents()
    {
        draggable = GetComponent<Draggable>();
        rigidBody = GetComponent<Rigidbody>();
    }

    void OnSelect(Selectable selectable, Vector3 selectMousePosition, RaycastHit hitInfo)
    {
        rigidBody.velocity = rigidBody.angularVelocity = Vector3.zero;
        dominantAxis = hitInfo.normal.DominantAxis();
        dragHitInfo = hitInfo;
    }

    void OnDrag(Draggable draggable, Drag drag)
    {
        Vector3 rawDragVector = (drag.MousePositionCurrent - drag.MousePositionLast.Value);
        Vector3 rotationVector = SwipeToRotationVector(rawDragVector, dominantAxis);
        Diagnostics.Log("adding vector " + rotationVector);
        rigidBody.AddForceAtPosition(rotationVector * .02f, dragHitInfo.point, ForceMode.Impulse);
    }

    void OnDragDeselect(Draggable draggable, Drag drag)
    {        

    }

    Vector3 SwipeToRotationVector(Vector3 rawDragVector, Axis faceDirection)
    {
        switch(faceDirection) {
            case Axis.Z:
            return new Vector3(rawDragVector.x, rawDragVector.y, 0f);
            case Axis.Y:
            return new Vector3(rawDragVector.x, rawDragVector.y, 0f);
            case Axis.X:
            return new Vector3(rawDragVector.x, rawDragVector.y, 0f);
        }
        return Vector3.zero;
    }
}
