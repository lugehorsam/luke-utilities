using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Draggable))]
[RequireComponent(typeof(RotationBinding))]

public class RotateOnDrag : MonoBehaviour {
    RotationBinding rotationBinding;
    Draggable draggable;

    [SerializeField]
    float unitsPerSwipePixel;

    [SerializeField]
    LerpAsset inertiaCurve;
    const float FRICTION = 2f;
    const float MAX_DRAG_VECTOR_LENGTH = 25f;
    const float MAX_OFFSET_VECTOR_LENGTH = 50f;


    void Awake()
    {
        draggable = GetComponent<Draggable>();
        draggable.OnDrag += OnDrag;
        draggable.OnSelect += OnSelect;
        draggable.OnDragDeselect += OnDragDeselect;
        rotationBinding = GetComponent<RotationBinding>();
    }

    void OnSelect(Selectable selectable, Vector3 selectMousePosition)
    {
        rotationBinding.StopAllLerps();
    }

    void OnDrag(Draggable draggable, Drag drag)
    {
        Vector3 swipeVector = drag.MousePositionCurrent - drag.MousePositionLast.Value;
        swipeVector = Vector3.ClampMagnitude(swipeVector, MAX_DRAG_VECTOR_LENGTH);
        Vector3 targetRotation = rotationBinding.GetProperty() + SwipeToRotationVector(swipeVector) * unitsPerSwipePixel;
        rotationBinding.SetProperty(targetRotation);
    }

    void OnDragDeselect(Draggable draggable, Drag drag)
    {
        Vector3 rawRotationOffset = (SwipeToRotationVector(drag.Velocity) / FRICTION) * unitsPerSwipePixel;
        Vector3 clampedOffset = Vector3.ClampMagnitude(rawRotationOffset, MAX_OFFSET_VECTOR_LENGTH);
        Vector3 targetProperty = rotationBinding.GetProperty() + clampedOffset;
        float targetTime = clampedOffset.magnitude/5f;
        FiniteLerp<Vector3> newLerp = new FiniteLerp<Vector3>(targetProperty, targetTime, inertiaCurve);
        rotationBinding.EnqueueFiniteLerp(newLerp, stopOtherEnumerators: true);
    }

    Vector3 SwipeToRotationVector(Vector3 swipeVector)
    {
        return new Vector3(swipeVector.y, -swipeVector.x, swipeVector.z);
    }
}
