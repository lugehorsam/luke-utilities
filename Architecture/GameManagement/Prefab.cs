using UnityEngine;
using System;

namespace Utilities
{
    [Serializable]
    public class Prefab {

        [SerializeField]
        GameObject prefab;

        [SerializeField]
        Transform instantiationHolder;

        [SerializeField]
        Vector3 localPosition;

        public GameObject Instantiate ()
        {
            GameObject instance = prefab == null ? new GameObject() : GameObject.Instantiate (prefab, Vector2.zero, Quaternion.Euler (Vector3.zero)) as GameObject;
            if (instantiationHolder != null)
            {
                instance.transform.SetParent(instantiationHolder, worldPositionStays: false);
            }
            instance.transform.localPosition = localPosition;

            return instance;
        }

        public T Instantiate<T> (Transform holder = null) where T : Component
        {
            return Instantiate ().GetOrAddComponent<T>();
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

}
