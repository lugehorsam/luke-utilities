namespace Utilities.Bindings
{
    using UnityEngine;

    public abstract class Property<T, K> : Property where K : Component
    {
        [SerializeField] private T _initialProperty;

        protected abstract T Get(K component);
        protected abstract void Set(K component, T property);
        
        protected virtual T ProcessInitialProperty(T property)
        {
            return property;
        }

        public sealed override void Init(GameObject gameObject)
        {
            Set(gameObject.GetComponent<K>(), ProcessInitialProperty(_initialProperty));
        }
    }

    public abstract class Property : ScriptableObject
    {
        public abstract void Init(GameObject gameObject);
    }
}
