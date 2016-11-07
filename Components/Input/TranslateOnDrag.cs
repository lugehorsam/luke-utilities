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
        draggable.OnDrag += OnDrag;
        draggable.OnDeselect += OnDeselect;
        draggable.OnSelect += OnSelect;
    }

    void OnSelect(Selectable selectable, Vector3 selectablePosition)
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(selectablePosition);
        offsetFromMouse = worldPoint - transform.position;
    }

    void OnDrag(Draggable draggable, Drag drag)
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(drag.MousePositionCurrent);
        Vector3 newPosition = worldPoint - offsetFromMouse;
        positionBinding.SetProperty(drag.MousePositionCurrent);
    }

    void OnDeselect(Selectable selectable)
    {
        offsetFromMouse = Vector3.zero;
    }
}
