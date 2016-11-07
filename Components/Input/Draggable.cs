using UnityEngine;
using System.Linq;
using System;

[RequireComponent(typeof(MultiCollider))]
public class Draggable : Selectable {

    public event Action<Draggable, Vector3, Vector3> OnDrag = (arg1, arg2, arg3) => { };
    public event Action<Draggable, Collider, Vector3> OnDragLeave = (arg1, arg2, arg3) => { };

    RotationBinding rotationBinding;

    public bool MoveToDragPosition
    {
        get
        {
            return moveToDragPosition;
        }
        set
        {
            moveToDragPosition = value;
        }
    }

    [SerializeField]
    bool moveToDragPosition;

    Vector3 offsetFromMouse;

    MultiCollider multiCollider;
    Collider [] currentColliders = new Collider [] { };

    Vector3? initialMouseSelectPosition;

    void Awake ()
    {
        multiCollider = GetComponent<MultiCollider> ();
        rotationBinding = GetComponent<RotationBinding>();
    }

    protected sealed override void HandleOnSelect(Vector3 mousePosition) {
        Vector3 worldPoint = MousePositionToWorldPoint (mousePosition);      
        offsetFromMouse = worldPoint - transform.position;
        initialMouseSelectPosition = mousePosition;
    }

    protected sealed override void HandleOnHold (Vector3 mousePosition)
    {
        Vector3 worldPoint = MousePositionToWorldPoint (mousePosition);
        Vector3 oldPosition = transform.position;
        Vector3 newPosition = worldPoint - offsetFromMouse;

        if (oldPosition != newPosition) {
            if (MoveToDragPosition)
                transform.position = newPosition;
            HandleOnDrag (mousePosition);
            OnDrag (this, oldPosition, mousePosition);
        }
    }

    protected sealed override void HandleOnDeselect(Vector3 mousePosition) {
        offsetFromMouse = Vector3.zero;
    }
 
    void HandleOnDrag(Vector3 mousePosition) {
        Diagnostics.Log("On drag");

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
    