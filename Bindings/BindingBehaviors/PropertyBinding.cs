using UnityEngine;

namespace Utilities
{
    [ExecuteInEditMode]
    public abstract class PropertyBinding<TProperty, TComponent> : MonoBehaviour where TComponent : Component
    {
        [SerializeField] protected TProperty _startProperty;

        protected TComponent _Component => GetComponent<TComponent>(); //TODO cache this and throw relevant exceptions

        public abstract TProperty GetProperty();
        public abstract void SetProperty(TProperty property);

        private void Start()
        {
            SetProperty(_startProperty);
        }

        private void Update()
        {
            if (Application.isEditor)
            {
                SetProperty(_startProperty);
            }
        }
    }  
}
