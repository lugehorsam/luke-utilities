namespace Utilities.Bindings
{
    using System;
    using UnityEngine;

    [ExecuteInEditMode]
    public abstract class PropertyBinding<TProperty, TComponent> : MonoBehaviour
        where TComponent : Component
    {        
        [SerializeField] private ScriptableObject _propertyObject;
        
        private TComponent _component;

        protected TComponent _Component => _component ?? (_component = GetComponent<TComponent>());

        public abstract TProperty GetProperty();
        public abstract void SetProperty(TProperty property);

        private void Awake()
        {
            OnPropertyChanged();
        }

        private void OnValidate()
        {
            Diag.Log("on validate " + this.gameObject.name);
            OnPropertyChanged();

#if UNITY_EDITOR
            if (_propertyObject == null)
            {
                return;
            }

            PropertyObject<TProperty> propertyObject = TryCastPropertyObject();

            propertyObject.OnPropertyChanged -= OnPropertyChanged;
            propertyObject.OnPropertyChanged += OnPropertyChanged;
#endif
        }

        private void OnPropertyChanged()
        {
            Diag.Log("ON PROPERTY CHANGED " + gameObject.name);
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
        
        private void ApplyPropertyObject()
        {
            if (_propertyObject == null)
                return;

            var propertyObject = TryCastPropertyObject();
            
            SetProperty(propertyObject.Property);
            Diag.Log("set property");
        }

        private PropertyObject<TProperty> TryCastPropertyObject()
        {
            PropertyObject<TProperty> propertyObject = _propertyObject as PropertyObject<TProperty>;
            
            if (propertyObject == null)
                throw new NullReferenceException($"Object {_propertyObject} on binding {this} could not be casted to a {typeof(PropertyObject<>)}.");

            return propertyObject;
        }
    }  
}
