using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Draggable))]
[RequireComponent(typeof(RotationBinding))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class RotateOnDrag : GameBehavior {
    
    Draggable draggable;
    Rigidbody rigidBody;

    [SerializeField]
    float rotationUnitsPerSwipePixel;

    Drag currentDrag = null;

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

    void OnSelect(Selectable selectable, Vector3 selectMousePosition)
    {
        rigidBody.velocity = rigidBody.angularVelocity = Vector3.zero;
    }

    void OnDrag(Draggable draggable, Drag drag)
    {
        Vector3 rawDragVector = (drag.MousePositionCurrent - drag.MousePositionLast.Value);
        Vector3 rotationVector = SwipeToRotationVector(rawDragVector);
        rigidBody.AddTorque(rotationVector * .02f, ForceMode.Impulse);

        if (currentDrag == null) {
            currentDrag = drag;
        }
    }

    void OnDragDeselect(Draggable draggable, Drag drag)
    {        

    }

    Vector3 SwipeToRotationVector(Vector3 swipeVector)
    {
        return new Vector3(swipeVector.y, -swipeVector.x, 0f);
    }
}
