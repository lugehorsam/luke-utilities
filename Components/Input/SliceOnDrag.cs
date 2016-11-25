using UnityEngine;

[RequireComponent(typeof(TouchDispatcher))]
public class SliceOnDrag : GameBehavior {

    TouchDispatcher draggable;

    protected sealed override void AddEventHandlers()
    {
        draggable.OnRelease += OnDragEnd;
    }

    protected sealed override void RemoveEventHandlers()
    {
        draggable.OnRelease -= OnDragEnd;
    }

    void OnDragEnd(TouchDispatcher draggable, AbstractGesture currentDrag) {
        Diagnostics.Log("current drag is " + currentDrag);
    }
}
