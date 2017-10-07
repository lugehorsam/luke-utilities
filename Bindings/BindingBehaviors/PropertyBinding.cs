namespace Utilities
{
    using UnityEngine;

    public abstract class PropertyBinding<TProperty, TComponent> : MonoBehaviour where TComponent : Component
    {
        private TComponent _component;
        
        protected TComponent _Component => _component ?? (_component = GetComponent<TComponent>());

        public abstract TProperty GetCurrentProperty();
        public abstract void SetProperty(TProperty property);
    }  
}
