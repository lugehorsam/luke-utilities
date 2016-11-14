using UnityEngine;
using System.Collections;
using System;
using System.Linq;

[RequireComponent(typeof(Collider))]
public class Selectable : GameBehavior {

    public event Action<Selectable, Vector3, RaycastHit> OnSelect = (arg1, arg2, arg3) => { };
    public event Action OnHold = () => { };
    public event Action<Selectable> OnDeselect = (obj) => { };

    public bool Selected {
        get {
            return selected;
        }
    }

    bool selected;

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

    protected sealed override void InitComponents()
    {
        colliderComponent = GetComponent<Collider>();
        InitSelectableComponents();
    }

    protected virtual void InitSelectableComponents()
    {

    }

    void Update() {

        RaycastHit hitInfo;
        bool mouseDown = Input.GetMouseButton(0);
        bool mouseOver = IsMouseOver(out hitInfo);

        bool firstClick = !selected && mouseOver;
        bool hold = selected && mouseDown;
        bool release = selected && !mouseDown;

        if (firstClick)
        {
            initialMouseSelectPosition = Input.mousePosition;
            HandleOnSelect(Input.mousePosition);
            OnSelect(this, Camera.main.ScreenToWorldPoint(Input.mousePosition), hitInfo);
            selected = true;
        }
        else if (hold)
        {
            HandleOnHold(Input.mousePosition);
            OnHold();
        }
        else if (release)
        {
            HandleOnDeselect(Input.mousePosition);
            OnDeselect(this);
            selected = false;
        }      
    }
        
    bool IsMouseOver(out RaycastHit hitInfo) {
        hitInfo = default(RaycastHit);
        Vector3 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(origin, Vector3.forward, 100f);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.collider == colliderComponent) {
                hitInfo = hit;
                return true;
            }
        }
        return false;
    }
}
