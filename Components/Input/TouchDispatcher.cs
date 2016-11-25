using UnityEngine;
using System;
using System.Collections.Generic;

public class TouchDispatcher : GameBehavior
{
    [SerializeField]
    protected new Collider collider;

    public event Action<TouchDispatcher, AbstractGesture> OnTouch = (arg1, arg2) => { };
    public event Action<TouchDispatcher, AbstractGesture> OnHold = (arg1, arg2) => { };
    public event Action<TouchDispatcher, AbstractGesture> OnRelease = (arg1, arg2) => { };
    public event Action<TouchDispatcher, AbstractGesture> OnDrag = (arg1, arg2) => { };

    protected virtual void HandleOnTouch(TouchDispatcher touchDispatcher, AbstractGesture gestureFrame) { }
    protected virtual void HandleOnHold(TouchDispatcher touchDispatcher, AbstractGesture gestureFrame) { }
    protected virtual void HandleOnRelease(TouchDispatcher touchDispatcher, AbstractGesture gestureFrame) { }
    protected virtual void HandleOnDrag(TouchDispatcher touchDispatcher, AbstractGesture gestureFrame) { }

    protected AbstractGesture currentGesture;

    void Update()
    {
        bool mouseIsDown = Input.GetMouseButton(0);
        bool mouseWasDown = currentGesture != null;

        RaycastHit overlapInfo;

        bool overlap = IsOverlapped(out overlapInfo, Input.mousePosition);
        bool firstTouch = !mouseWasDown && mouseIsDown;
        bool hold = mouseWasDown && mouseIsDown;
        bool release = mouseIsDown && !mouseIsDown;
        bool drag = hold && currentGesture.MousePositionCurrent != currentGesture.MousePositionLast;

        if (firstTouch)
        {
            currentGesture = new Gesture();
            HandleOnTouch(this, currentGesture);
            OnTouch(this, currentGesture);
        }

        if (firstTouch || hold)
        {
            GestureFrame gestureFrame = new GestureFrame(Input.mousePosition, overlapInfo);
            currentGesture.AddGestureFrame(gestureFrame);
            HandleOnHold(this, currentGesture);
            OnHold(this, currentGesture);
        }

        if (drag)
        {
            HandleOnDrag(this, currentGesture);
            OnDrag(this, currentGesture);
        }

        if (release)
        {
            HandleOnRelease(this, currentGesture);
            OnRelease(this, currentGesture);
        }    
    }

    bool IsOverlapped(out RaycastHit hitInfo, Vector3 mousePosition)
    {
        hitInfo = default(RaycastHit);
        Vector3 origin = Camera.main.ScreenToWorldPoint(mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(origin, Vector3.forward, 100f);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.collider == collider)
            {
                hitInfo = hit;
                return true;
            }
        }
        return false;
    }
}
