using UnityEngine;
using System.Linq;
using System;

[RequireComponent(typeof(MultiCollider2D))]
public class Draggable : Selectable {

    public event Action<Draggable, Vector3> OnDrag = (arg1, arg2) => { };
    public event Action<Draggable, Collider2D, Vector3> OnDragLeave = (arg1, arg2, arg3) => { };
    Vector3 offsetFromMouse;

    MultiCollider2D multiCollider;
    Collider2D [] currentColliders = new Collider2D [] { };

    void Awake ()
    {
        multiCollider = GetComponent<MultiCollider2D> ();
    }

    protected sealed override void HandleOnSelect(Vector3 mousePosition) {
        Vector3 worldPoint = MousePositionToWorldPoint (mousePosition);      
        offsetFromMouse = worldPoint - transform.position;
    }

    protected sealed override void HandleOnHold (Vector3 mousePosition)
    {
        Vector3 worldPoint = MousePositionToWorldPoint (mousePosition);
        Vector3 oldPosition = transform.position;
        Vector3 newPosition = worldPoint - offsetFromMouse;

        if (oldPosition != newPosition) {
            transform.position = newPosition;
            HandleOnDrag (mousePosition);
            OnDrag (this, mousePosition);
        }
    }

    protected sealed override void HandleOnDeselect(Vector3 mousePosition) {
        offsetFromMouse = Vector3.zero;
    }
 
    void HandleOnDrag(Vector3 mousePosition) {
        Collider2D [] oldColliders = currentColliders.Except (multiCollider.OtherColliders).ToArray ();
        Collider2D [] newColliders = multiCollider.OtherColliders.Except (currentColliders).ToArray ();

        currentColliders = multiCollider.OtherColliders;

        foreach (Collider2D oldCollider in oldColliders) {
            OnDragLeave (this, oldCollider, mousePosition);
        }

        foreach (Collider2D newCollider in newColliders) {
            /// on drag enter
        }

    }

    Vector3 MousePositionToWorldPoint(Vector3 mousePosition) {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint (mousePosition);
        worldPoint.z = transform.position.z;
        return worldPoint;
    }
}
    