using System.Linq;

namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    
    [Serializable]
    public class Prefab 
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private bool _dynamic;
        [SerializeField] private Transform _parent;
        [SerializeField] private Vector3 _localPosition;
        [SerializeField] private List<GameObject> _instances;
        
        public bool AnyInstances => _instances.Any();
        
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

        GameObject CreateGameObject()
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

        public virtual GameObject Instantiate ()
        {
            var instance = CreateGameObject();
            InitGameObject(instance);
            _instances.Add(instance);
            return instance;
        }

        public virtual T Instantiate<T> () where T : Component
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
            return _prefab == gameObject;
        }

        public bool IsPrefabOf<T> (T gameObject) where T : Component
        {
            return _prefab.GetComponent<T>() == gameObject.GetComponent<T>();
        }

        public void DestroyInstances(bool immediate)
        {
            var instancesToDestroy = new List<GameObject>(_instances);
                        
            foreach (var instance in instancesToDestroy)
            {
                if (immediate)
                    GameObject.DestroyImmediate(instance);
                else
                    GameObject.Destroy(instance);
            }

            _instances.Clear();
        }

    }
}
