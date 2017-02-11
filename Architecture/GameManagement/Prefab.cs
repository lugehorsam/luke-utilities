using UnityEngine;
using System;

namespace Utilities
{
    [Serializable]
    public class Prefab {

        [SerializeField] GameObject prefab;
        [SerializeField] private bool dynamicGameObject;
        [SerializeField] Transform instantiationHolder;
        [SerializeField] Vector3 localPosition;

        public Prefab(Transform instantiationHolder, Vector3 localPosition)
        {
            this.instantiationHolder = instantiationHolder;
            this.localPosition = localPosition;
        }

        public Prefab(Transform instantiationHolder)
        {
            this.instantiationHolder = instantiationHolder;
            localPosition = Vector3.zero;
        }

        GameObject CreateGameObject()
        {
            GameObject instance = dynamicGameObject ? new GameObject() : GameObject.Instantiate (prefab, Vector2.zero, Quaternion.Euler (Vector3.zero)) as GameObject;
            if (instantiationHolder != null)
            {
                instance.transform.SetParent(instantiationHolder, worldPositionStays: false);
            }
            instance.transform.localPosition = localPosition;
            return instance;
        }

        public virtual GameObject Instantiate ()
        {
            var instance = CreateGameObject();
            InitGameObject(instance);
            return instance;
        }

        public virtual T Instantiate<T> (Transform holder = null) where T : Component
        {
            var instance = CreateGameObject();
            T component = instance.GetOrAddComponent<T>();
            InitGameObject(instance);
            return component;
        }

        void InitGameObject(GameObject gameObject)
        {
            var instantiateds = gameObject.GetComponentsWithInterface<IInstantiated>();
            foreach (var instantiated in instantiateds)
                instantiated.HandleOnInstantiated();
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
