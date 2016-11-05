using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SerializableKeyValuePair<T, K> {

    public T Key {
        get {
            return key;
        }
    }

    public K Value {
        get {
            return value;
        }
    }

    [SerializeField]
    T key;

    [SerializeField]
    K value;

}
