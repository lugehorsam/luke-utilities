using UnityEngine;
using System;
using System.Collections.ObjectModel;

public class TouchDispatcher : GameBehavior
{
    new Collider collider;

    public event Action<TouchDispatcher, Gesture> OnTouch = (arg1, arg2) => { };
    public event Action<TouchDispatcher, Gesture> OnHold = (arg1, arg2) => { };
    public event Action<TouchDispatcher, Gesture> OnRelease = (arg1, arg2) => { };
    public event Action<TouchDispatcher, Gesture> OnDrag = (arg1, arg2) => { };
    public event Action<TouchDispatcher, Gesture> OnDragLeave = (arg1, arg2) => { };

    protected virtual void HandleOnTouch(TouchDispatcher touchDispatcher, Gesture gestureFrame) { }
    protected virtual void HandleOnHold(TouchDispatcher touchDispatcher, Gesture gestureFrame) { }
    protected virtual void HandleOnRelease(TouchDispatcher touchDispatcher, Gesture gestureFrame) { }
    protected virtual void HandleOnDrag(TouchDispatcher touchDispatcher, Gesture gestureFrame) { }
    protected virtual void HandleOnDragLeave(TouchDispatcher touchDispatcher, Gesture gestureFrame) { }

    protected Gesture currentGesture;

    void Awake()
    {
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        bool mouseIsDown = Input.GetMouseButton(0);
        bool mouseWasDown = currentGesture != null;

        RaycastHit? hitInfo = GetHitInfo(Input.mousePosition);

        bool firstTouch = !mouseWasDown && mouseIsDown;
        bool hold = mouseWasDown && mouseIsDown;
        bool release = mouseWasDown && !mouseIsDown;
        bool drag = hold && currentGesture.MousePositionCurrent != currentGesture.MousePositionLast;
        if (firstTouch)
        {
            currentGesture = new Gesture();
            HandleOnTouch(this, currentGesture);
            OnTouch(this, currentGesture);
        }

        if (firstTouch || hold)
        {
            GestureFrame gestureFrame = new GestureFrame(Input.mousePosition, hitInfo);
            currentGesture.AddGestureFrame(gestureFrame);
            HandleOnHold(this, currentGesture);
            OnHold(this, currentGesture);
        }


        if (drag)
        {
            HandleOnDrag(this, currentGesture);
            OnDrag(this, currentGesture);
            bool collisionLastFrame = currentGesture.LastFrame.Value.HitForCollider(collider).HasValue;
            bool collisionThisFrame = currentGesture.CurrentFrame.HitForCollider(collider).HasValue;
            Diagnostics.Log("collision last frame is " + collisionLastFrame + " and collision this frame is " + collisionThisFrame);

            if (collisionLastFrame && !collisionThisFrame)
            {
                HandleOnDragLeave(this, currentGesture);
                OnDragLeave(this, currentGesture);
                Diagnostics.Log("dispatching");
            }
        }

        if (release)
        {
            HandleOnRelease(this, currentGesture);
            OnRelease(this, currentGesture);
            currentGesture = null;
        }    
    }

    RaycastHit? GetHitInfo(Vector3 mousePosition)
    {
        Vector3 origin = Camera.main.ScreenToWorldPoint(mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(origin, Vector3.forward, 100f);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.collider == collider)
            {
                return hit;
            }
        }
        return null;
    }
}
