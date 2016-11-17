using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Draggable))]
[RequireComponent(typeof(PositionBinding))]
public class TranslateOnDrag : MonoBehaviour {

    Draggable draggable;
    PositionBinding positionBinding;
    Vector3 offsetFromMouse;

    void Awake()
    {
        draggable = GetComponent<Draggable>();
        positionBinding = GetComponent<PositionBinding>();
        draggable.OnMotion += OnDrag;
        draggable.OnDeselect += OnDeselect;
    }

    void OnSelect(Selectable selectable, Vector3 selectablePosition, RaycastHit hitInfo)
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(selectablePosition);
        offsetFromMouse = worldPoint - transform.position;
    }

    void OnDrag(Draggable draggable, Motion drag, RaycastHit hitInfo)
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(drag.MousePositionCurrent);
        Vector3 newPosition = worldPoint - offsetFromMouse;
        positionBinding.SetProperty(newPosition);
    }

    void OnDeselect(Selectable selectable)
    {
        offsetFromMouse = Vector3.zero;
    }
}
