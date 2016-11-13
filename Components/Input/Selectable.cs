using UnityEngine;
using System.Collections;
using System;
using System.Linq;

[RequireComponent(typeof(Collider))]
public class Selectable : GameBehavior {

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

    Collider colliderComponent;

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

    protected override void InitComponents()
    {
        base.InitComponents();
        colliderComponent = GetComponent<Collider>();
    }

    void Update() {

        if (IsMouseDownOver())
        {
            Diagnostics.Log("mouse is down over");

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
            else
            {
                HandleOnHold(Input.mousePosition);
                if (OnHold != null)
                {
                    OnHold();
                }
            }
        }
        else {
            if (selected)
            {
                HandleOnDeselect(Input.mousePosition);
                if (OnDeselect != null)
                {
                    OnDeselect(this);
                }
                selected = false;
            }
        }
    }
        
    bool IsMouseDownOver() {
        if (Input.GetMouseButton(0)) {
            Vector3 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Diagnostics.Log("origin is " + origin);
            RaycastHit[] hits = Physics.RaycastAll(origin, Vector3.forward, 100f);
            Diagnostics.Log("Hits are " + hits.ToFormattedString());
            bool colliderHit = hits.Any((hit) => {
                Diagnostics.Log("Collider is " + hit.collider);
                Diagnostics.Log("Collider is " + colliderComponent);

                return hit.collider == this.colliderComponent;
            });
            if (colliderHit) {
                Diagnostics.Log("Returning true");
                return true;
            }
        }
        return false;
    }
}
