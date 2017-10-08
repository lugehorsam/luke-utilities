namespace Utilities.Bindings
{
    using System;
    using UnityEngine;

    [ExecuteInEditMode]
    public abstract class PropertyBinding<TProperty, TComponent> : MonoBehaviour where TComponent : Component
    {
        [SerializeField] private ScriptableObject _propertyObject;
        
        private TComponent _component;

        protected TComponent _Component
        {
            get
            {
                return _component ?? (_component = GetComponent<TComponent>());
            }
        }

        public abstract TProperty GetProperty();
        public abstract void SetProperty(TProperty property);

        private void Awake()
        {
            ApplyPropertyObject();
        }

        public void OnGUI()
        {
            if (!isActiveAndEnabled)
                return;
            
            try
            {
                ApplyPropertyObject();
            }  
            catch (Exception e) when (e is MissingReferenceException || 
                                      e is MissingComponentException || 
                                      e is UnassignedReferenceException)
            {                
                _component = GetComponent<TComponent>();
                ApplyPropertyObject();
            }
        }
        
        public void ApplyPropertyObject()
        {
            if (_propertyObject == null)
                return;
			
            PropertyObject<TProperty> propertyObject = _propertyObject as PropertyObject<TProperty>;
			
            if (propertyObject == null)
                throw new NullReferenceException($"Object {_propertyObject} on binding {this} could not be casted to a {typeof(PropertyObject<>)}.");

            SetProperty(propertyObject.Property);
        }
    }  
}
