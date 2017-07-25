﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Utilities
{
    [RequireComponent(typeof(Collider))]
    public class MultiCollider : MonoBehaviour
    {

        public Collider[] OtherColliders => otherColliders.ToArray();

        List<Collider> otherColliders = new List<Collider>();
        Collider colliderComponent;

        public Action OnCollision;

        void Awake()
        {
            colliderComponent = GetComponent<Collider>();
            if (!colliderComponent.isTrigger)
            {
                colliderComponent.isTrigger = true;
                Diagnostics.LogWarning("Multicollider on " + gameObject.name +
                                       " not attached to a trigger collider. Force setting collider to trigger.");
            }
        }

        void OnTriggerEnter(Collider other)
        {
            otherColliders.Add(other);
            if (OnCollision != null)
            {
                OnCollision();
            }
        }

        void OnTriggerExit(Collider other)
        {
            otherColliders.Remove(other);
            if (OnCollision != null)
            {
                OnCollision();
            }
        }

        //TODO handle multiple right colliders
        public Collider GetRightOverlap()
        {
            return otherColliders.FirstOrDefault(
                (othercollider) => othercollider.bounds.center.x >= colliderComponent.bounds.center.x);
        }

        //TODO handle multiple left colliders
        public Collider GetLeftOverlap()
        {
            return otherColliders.FirstOrDefault((othercollider) =>
            {
                return othercollider.bounds.center.x <= colliderComponent.bounds.center.x;
            });
        }
    }
}
   