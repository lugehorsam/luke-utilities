
namespace Utilities
{
    using System;
    using UnityEngine;
    using Utilities.Bindings;
    
    [Serializable]
    public sealed class Prefab 
    {
        [SerializeField] private GameObject _prefab;
        
        public Prefab(GameObject gameObjectToWrap)
        {
            _prefab = gameObjectToWrap;
        }

        private GameObject CreateGameObject(Transform parent)
        {
            GameObject instance = GameObject.Instantiate (_prefab, Vector2.zero, Quaternion.Euler (Vector3.zero));
            
            if (instance == null)
                throw new NullReferenceException("No associated prefab.");
            
            if (parent != null)
            {
                instance.transform.SetParent(parent, worldPositionStays: false);
            }
            
            return instance;
        }

        public GameObject Instantiate (Transform parent)
        {
            return CreateGameObject(parent);
        }

        public T Instantiate<T> (Transform parent) where T : Component
        {
            Diag.Crumb(this, "Instantiating " + this);
            var instance = CreateGameObject(parent);
            T component = instance.GetOrAddComponent<T>();
            return component;
        }

        public bool IsPrefabOf (GameObject gameObject)
        {
            return _prefab == gameObject;
        }

        public bool IsPrefabOf<T> (T gameObject) where T : Component
        {
            return _prefab.GetComponent<T>() == gameObject.GetComponent<T>();
        }

        public override string ToString()
        {
            return _prefab == null ? base.ToString() : _prefab.name;
        }
    }
}
