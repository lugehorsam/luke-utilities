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
    float maxVectorLength;
    [SerializeField]
    float inertia;

    void Awake()
    {
        draggable = GetComponent<Draggable>();
        draggable.OnDrag += OnDrag;
        draggable.OnSelect += OnSelect;
        draggable.OnDeselect += OnDeselect;
        rotationBinding = GetComponent<RotationBinding>();
    }

    void OnSelect(Selectable selectable, Vector3 selectMousePosition)
    {
        rotationBinding.StopAllLerps();
    }

    void OnDrag(Draggable draggable, Drag drag)
    {
        Vector3 swipeVector = drag.MousePositionCurrent - drag.MousePositionLast.Value;
        swipeVector = Vector3.ClampMagnitude(swipeVector, maxVectorLength);
        Vector3 targetRotation = rotationBinding.GetProperty() + new Vector3(swipeVector.y, -swipeVector.x, swipeVector.z) * unitsPerSwipePixel;
        rotationBinding.SetProperty(targetRotation);
    }

    void OnDeselect(Selectable selectable)
    {

    }
}
