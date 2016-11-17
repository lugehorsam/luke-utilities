using UnityEngine;

[RequireComponent(typeof(Draggable))]
public class SliceOnDrag : GameBehavior {

    Draggable draggable;

    protected sealed override void AddEventHandlers()
    {
        draggable.OnDragEnd += OnDragEnd;
    }

    protected sealed override void RemoveEventHandlers()
    {
        draggable.OnDragEnd -= OnDragEnd;
    }

    void OnDragEnd(Draggable draggable, Drag currentDrag) {
        Diagnostics.Log("current drag is " + currentDrag);
    }
}
