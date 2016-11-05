using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

[RequireComponent(typeof(Collider2D))]
public class MultiCollider2D : MonoBehaviour {

    public Collider2D [] OtherColliders {
        get {
            return otherColliders.ToArray ();
        }
    }

    List<Collider2D> otherColliders = new List<Collider2D>();
    Collider2D colliderComponent;

    public Action OnCollision;

    void Awake() {
        colliderComponent = GetComponent<Collider2D> ();
        if (!colliderComponent.isTrigger) {
            colliderComponent.isTrigger = true;
            Debug.LogWarning ("Multicollider on " + gameObject.name + " not attached to a trigger collider. Force setting collider to trigger.");
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        otherColliders.Add (other);
        if (OnCollision != null) {
            OnCollision ();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        otherColliders.Remove (other);
        if (OnCollision != null) {
            OnCollision ();
        }
    }

    //TODO handle multiple right colliders
    public Collider2D GetRightOverlap() {
        return otherColliders.FirstOrDefault ((othercollider) => othercollider.bounds.center.x >= colliderComponent.bounds.center.x);
    }

    //TODO handle multiple left colliders
    public Collider2D GetLeftOverlap() {
        return otherColliders.FirstOrDefault ((othercollider) => {
             return othercollider.bounds.center.x <= colliderComponent.bounds.center.x;
        });
    }

    void Update ()
    {
    }
}
   