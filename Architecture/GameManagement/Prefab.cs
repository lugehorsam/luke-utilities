using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Prefab {

    [SerializeField]
    GameObject prefab;

    [SerializeField]
    Transform holder;

    [SerializeField]
    Vector3 localPosition;

    public GameObject Instantiate ()
    {
        GameObject instance = GameObject.Instantiate (prefab, Vector2.zero, Quaternion.Euler (Vector3.zero)) as GameObject;
        if (holder != null)
        {
            instance.transform.SetParent(holder, worldPositionStays: false);
        }
        instance.transform.localPosition = localPosition;
        return instance;
    }

    public T Instantiate<T> (Transform holder = null) where T : Component
    {
        return Instantiate ().GetComponent<T> ();
    }

    public bool IsPrefabOf (GameObject gameObject)
    {
        return prefab == gameObject;
    }

    public bool IsPrefabOf<T> (T gameObject) where T : Component
    {
        return prefab.GetComponent<T>() == gameObject.GetComponent<T>();
    }
}
