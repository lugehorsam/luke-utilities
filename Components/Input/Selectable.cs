using UnityEngine;
using System.Collections;
using System;
using System.Linq;

[RequireComponent(typeof(Collider))]
public class Selectable : MonoBehaviour {

    public event Action<Selectable, Vector3> OnSelect;
    public event Action OnHold;
    public event Action<Selectable> OnDeselect;

    public bool Selected {
        get {
            return selected;
        }
    }

    bool selected;
    bool mouseDownOver;

    Collider2D colliderComponent;

    protected virtual void HandleOnHold(Vector3 mousePosition) {}
    protected virtual void HandleOnDeselect(Vector3 mousePosition) {}
    protected virtual void HandleOnSelect(Vector3 mousePosition) {}

    public Vector3? InitialMouseSelectPosition
    {
        get
        {
            return initialMouseSelectPosition;
        }
    }
    Vector3? initialMouseSelectPosition;

    void Awake() {
        colliderComponent = GetComponent<Collider2D> ();
    }

    void OnMouseDown () {
        bool wasSelected = selected;
        selected = true;
        if (!wasSelected)
        {
            initialMouseSelectPosition = Input.mousePosition;

            HandleOnSelect(Input.mousePosition);
            if (OnSelect != null)
            {
                OnSelect(this, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }

    void OnMouseUp() {
        if (selected) {
            HandleOnDeselect (Input.mousePosition);
            if (OnDeselect != null) {
                OnDeselect (this);
            }
            selected = false;
        }
    }

    void Update() {
        if (selected) {
            HandleOnHold (Input.mousePosition);
            if (OnHold != null) {
                OnHold ();
            }
        }
    }
        
    bool IsMouseDownOver() {
        if (Input.GetMouseButton(0)) {
            Vector2 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(origin, Vector2.zero);
            if (hits.Any((hit) => hit.collider == this.colliderComponent)) {
                return true;
            }
        }
        return false;
    }
}
