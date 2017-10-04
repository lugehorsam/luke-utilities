namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    
    [Serializable]
    public sealed class Prefab 
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private bool _dynamic;
        [SerializeField] private Transform _parent;
        [SerializeField] private Vector3 _localPosition;
                        
        public Prefab(Transform parent, Vector3 localPosition)
        {
            _parent = parent;
            _localPosition = localPosition;
        }

        public Prefab(Transform parent)
        {
            _parent = parent;
            _localPosition = Vector3.zero;
        }
        
        public Prefab(GameObject gameObjectToWrap)
        {
            _prefab = gameObjectToWrap;
        }

        private GameObject CreateGameObject()
        {
            GameObject instance = _dynamic ? new GameObject() : GameObject.Instantiate (_prefab, Vector2.zero, Quaternion.Euler (Vector3.zero));
            
            if (instance == null)
                throw new NullReferenceException("No associated prefab.");
            
            if (_parent != null)
            {
                instance.transform.SetParent(_parent, worldPositionStays: false);
            }
            
            instance.transform.localPosition = _localPosition;
            return instance;
        }

        public GameObject Instantiate ()
        {
            var instance = CreateGameObject();
            return instance;
        }

        public T Instantiate<T> () where T : Component
        {
            Diag.Crumb(this, "Instantiating " + this);
            var instance = CreateGameObject();
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
