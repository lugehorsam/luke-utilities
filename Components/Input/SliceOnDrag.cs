using UnityEngine;

[RequireComponent(typeof(Draggable))]
public class SliceOnDrag : GameBehavior {

    Draggable draggable;

    protected sealed override void AddEventHandlers()
    {
        draggable.OnMotion += OnDrag;
    }

    protected sealed override void RemoveEventHandlers()
    {
        draggable.OnMotion -= OnDrag;
    }

    void OnDrag(Draggable draggable, AbstractMotion drag, RaycastHit hitInfo) {
       
    }
}
