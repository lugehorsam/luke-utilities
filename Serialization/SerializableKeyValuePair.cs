using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SerializableKeyValuePair<T, K> {

    public T Key => key;

    public K Value => value;

    [SerializeField]
    T key;

    [SerializeField]
    K value;

}
