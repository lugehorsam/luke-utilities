using UnityEngine;
using System.Linq;
using System;

[RequireComponent(typeof(MultiCollider))]
public class Draggable : Selectable {

    public event Action<Draggable, Drag> OnDrag = (arg, arg2) => { };
    public event Action<Draggable, Collider, Vector3> OnDragLeave = (arg1, arg2, arg3) => { };
    public event Action<Draggable, Drag> OnDragEnd = (arg1, arg2) => { };
    public event Action<Draggable, Drag> OnDragDeselect = (arg1, arg2) => { };



    [SerializeField]
    float dragDetectFloor = 1f;

    MultiCollider multiCollider;
    Collider [] currentColliders = new Collider [] { };

    Drag currentDrag;

    void Awake ()
    {
        
        multiCollider = GetComponent<MultiCollider>();
    }

    protected sealed override void HandleOnSelect(Vector3 mousePosition) {
        currentDrag = new Drag(mousePosition);
    }

    protected sealed override void HandleOnHold (Vector3 mousePosition)
    {
        if (currentDrag == null)
        {
            currentDrag = new Drag(mousePosition);
        }
        else if (!currentDrag.MousePositionLast.HasValue)
        {
            currentDrag.MousePositionLast = mousePosition;
        }
        else if ((mousePosition - currentDrag.MousePositionLast.Value).magnitude > dragDetectFloor)
        {
            if (!currentDrag.ElapsedTime.HasValue)
            {
                currentDrag.ElapsedTime = 0f;
            }
            currentDrag.ElapsedTime += Time.deltaTime;
            Diagnostics.Log("Incremented elapsed time" + currentDrag.ElapsedTime);

            currentDrag.MousePositionCurrent = mousePosition;
            HandleOnDrag(mousePosition);
            OnDrag(this, currentDrag);
            currentDrag.MousePositionLast = mousePosition;
        }
        else {
            if (currentDrag.ElapsedTime.HasValue)
            {
                Diagnostics.Log("Elapsed time has valuea nd iti s " + currentDrag.ElapsedTime);
                OnDragEnd(this, currentDrag);
            }
            currentDrag = null;
        }
    }

    protected sealed override void HandleOnDeselect(Vector3 mousePosition) {
        if (currentDrag != null)
        {
            Diagnostics.Log("Dispatching drag from deselect " + currentDrag.ElapsedTime); 
            OnDragEnd(this, currentDrag);
            OnDragDeselect(this, currentDrag);
        }
        currentDrag = null;
    }
 
    void HandleOnDrag(Vector3 mousePosition) {
        Collider [] oldColliders = currentColliders.Except (multiCollider.OtherColliders).ToArray ();
        Collider [] newColliders = multiCollider.OtherColliders.Except (currentColliders).ToArray ();

        currentColliders = multiCollider.OtherColliders;

        foreach (Collider oldCollider in oldColliders) {
            OnDragLeave (this, oldCollider, mousePosition);
        }

        foreach (Collider newCollider in newColliders) {
            /// on drag enter
        }
    }

    Vector3 MousePositionToWorldPoint(Vector3 mousePosition) {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint (mousePosition);
        worldPoint.z = transform.position.z;
        return worldPoint;
    }
}
    