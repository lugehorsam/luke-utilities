using UnityEngine;
using System.Linq;
using System;

public class Draggable : Selectable {

    public event Action<Draggable, Motion, RaycastHit> OnMotion = (arg, arg2, arg3) => { };
    public event Action<Draggable, Motion> OnMotionEnd = (arg1, arg2) => { };
    public event Action<Draggable, MotionCollection> OnDragEnd = (arg1, arg2) => { };

    [SerializeField]
    float dragDetectFloor = 1f;

    Motion currentMotion;
    MotionCollection currentDrag;

    protected sealed override void HandleOnSelect(Vector3 mousePosition) {
        currentMotion = new Motion(mousePosition);
        currentDrag = new MotionCollection();
        currentDrag.AddMotion(currentMotion);
    }

    protected sealed override void HandleOnHold (Vector3 mousePosition, RaycastHit hitInfo)
    {
        if (currentMotion == null)
        {
            currentMotion = new Motion(mousePosition);
        }
        else if (!currentMotion.MousePositionLast.HasValue)
        {
            currentMotion.MousePositionLast = mousePosition;
        }
        else if ((mousePosition - currentMotion.MousePositionLast.Value).magnitude > dragDetectFloor)
        {
            if (!currentMotion.ElapsedTime.HasValue)
            {
                currentMotion.ElapsedTime = 0f;
            }
            currentMotion.ElapsedTime += Time.deltaTime;

            currentMotion.MousePositionCurrent = mousePosition;
            OnMotion(this, currentMotion, hitInfo);
            currentMotion.MousePositionLast = mousePosition;
        }
        else {
            if (currentMotion.ElapsedTime.HasValue)
            {
                OnMotionEnd(this, currentMotion);
            }
            currentMotion = null;
        }
    }

    protected sealed override void HandleOnDeselect(Vector3 mousePosition) {
        if (currentMotion != null)
        {
            OnMotionEnd(this, currentMotion);
            OnMotionEnd(this, currentMotion);
        }
        currentMotion = null;
    }
 
    Vector3 MousePositionToWorldPoint(Vector3 mousePosition) {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint (mousePosition);
        worldPoint.z = transform.position.z;
        return worldPoint;
    }
}
    