using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class CollectedBehavior<T, K> : MonoBehaviour 
    where T : class
    where K : class, ICollection<T>, new() {

    public static K Collection {
        get {
            return collection;
        }
    }

    static K collection;

    void Awake() {
        collection = collection ?? new K ();
        collection.Add (this as T);
    }        
}
