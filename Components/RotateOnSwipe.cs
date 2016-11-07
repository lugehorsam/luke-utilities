using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Draggable))]
[RequireComponent(typeof(RotationBinding))]

public class RotateOnSwipe : MonoBehaviour {
    RotationBinding rotationBinding;
    Draggable draggable;

    [SerializeField]
    float unitsPerSwipePixel;

    void Awake()
    {
        draggable = GetComponent<Draggable>();
        draggable.OnDrag += OnDrag;
        draggable.OnSelect += OnSelect;
        rotationBinding = GetComponent<RotationBinding>();
    }

    void OnSelect(Selectable selectable, Vector3 selectMousePosition)
    {
        rotationBinding.StopAllLerps();
    }

    void OnDrag(Draggable draggable, Vector3 oldMousePosition, Vector3 newMousePosition)
    {
        Vector3 swipeVector = newMousePosition - oldMousePosition;
        Vector3 targetRotation = rotationBinding.GetProperty() + swipeVector;
        Debug.Log("Enqueieing to " + targetRotation);
        rotationBinding.EnqueueFiniteLerp(new FiniteLerp<Vector3>(targetRotation, 1f), stopOtherEnumerators: true);
    }

	
}
