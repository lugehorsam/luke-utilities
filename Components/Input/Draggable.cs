using UnityEngine;
using System.Linq;
using System;

public class Draggable : Selectable {

    public event Action<Draggable, Drag, RaycastHit> OnDrag = (arg, arg2, arg3) => { };
    public event Action<Draggable, Drag> OnDragGestureEnd = (arg1, arg2) => { };
    public event Action<Draggable, Drag> OnDragEnd = (arg1, arg2) => { };
    public event Action<Draggable, Drag> OnDragLeave = (arg1, arg2) => { };

    [SerializeField]
    float dragDetectFloor = 1f;

    DragGesture currentDragGesture;
    Drag currentDrag;

    protected sealed override void HandleOnSelect(Vector3 mousePosition) {
        currentDrag = new Drag();
        CreateDragGesture(mousePosition);
    }

    protected sealed override void HandleOnHold (Vector3 mousePosition, RaycastHit hitInfo)
    {
        if (currentDragGesture == null)
        {
            Debug.Log("Creating gesture from hold " + LogType.Dragging);

            CreateDragGesture(mousePosition);
        }
        else 
        {

            bool firstDragUpdate = !currentDragGesture.MousePositionLast.HasValue;
            if (firstDragUpdate)
            {
                UpdateCurrentGesture(mousePosition);
            }
            else {
                bool enoughDragMagnitude = (mousePosition - currentDragGesture.MousePositionLast.Value).magnitude > dragDetectFloor;
                if (enoughDragMagnitude)
                {
                    UpdateCurrentGesture(mousePosition);
                    if (!firstDragUpdate)
                    {
                        OnDrag(this, currentDrag, hitInfo);
                    }
                }
                else
                {
                    currentDrag.AddGesture(currentDragGesture);
                    if (currentDragGesture.ElapsedTime > 0f)
                    {
                        OnDragGestureEnd(this, currentDrag);
                    }
                    currentDragGesture = null;
                }
            }
        }
    }

    void CreateDragGesture(Vector3 mousePosition)
    {
        currentDragGesture = new DragGesture(mousePosition);
        currentDrag.AddGesture(currentDragGesture);
    }

    void UpdateCurrentGesture(Vector3 mousePosition)
    {
        currentDragGesture.IncrementElapsedTime(Time.deltaTime);
        currentDragGesture.SetMousePositionCurrent(mousePosition);
    }

    protected sealed override void HandleOnDeselect(Vector3 mousePosition) {
        if (currentDragGesture != null)
        {
            OnDragGestureEnd(this, currentDrag);
            OnDragEnd(this, currentDrag);
        }
        currentDragGesture = null;
        currentDrag = null;
    }
 
    Vector3 MousePositionToWorldPoint(Vector3 mousePosition) {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint (mousePosition);
        worldPoint.z = transform.position.z;
        return worldPoint;
    }
}
    