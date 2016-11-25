using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TouchDispatcher))]
[RequireComponent(typeof(PositionBinding))]
public class TranslateOnDrag : MonoBehaviour {

    TouchDispatcher draggable;
    PositionBinding positionBinding;
    Vector3 offsetFromMouse;

    void Awake()
    {
        draggable = GetComponent<TouchDispatcher>();
        positionBinding = GetComponent<PositionBinding>();
        draggable.OnDrag += OnDrag;
        draggable.OnRelease += OnDeselect;
    }

    void OnSelect(TouchDispatcher listener, AbstractGesture gesture)
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(gesture.MousePositionCurrent);
        offsetFromMouse = worldPoint - transform.position;
    }

    void OnDrag(TouchDispatcher draggable, AbstractGesture drag)
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(drag.MousePositionCurrent);
        Vector3 newPosition = worldPoint - offsetFromMouse;
        positionBinding.SetProperty(newPosition);
    }

    void OnDeselect(TouchDispatcher draggable, AbstractGesture drag)
    {
        offsetFromMouse = Vector3.zero;
    }
}
