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

        protected PropertyObject<T> TryCastPropertyObject<T>(ScriptableObject propertyObject)
        {
            PropertyObject<T> castedPropertyObject = propertyObject as PropertyObject<T>;
            
            if (castedPropertyObject == null)
                throw new NullReferenceException($"Object {propertyObject} on binding {this} could not be casted to a {typeof(PropertyObject<T>)}.");

            return castedPropertyObject;
        }

        private TProperty GetPropertyFromObject(ScriptableObject propertyObject)
        {
            var castedPropertyObject = TryCastPropertyObject<TProperty>(_propertyObject);
            return castedPropertyObject.Property;
        }
        
        private void Awake()
        {
            OnPropertyChanged();
        }

        private void OnValidate()
        {
            OnPropertyChanged();

#if UNITY_EDITOR
            if (_propertyObject == null)
            {
                return;
            }

            PropertyObject<TProperty> propertyObject = TryCastPropertyObject<TProperty>(_propertyObject);

            propertyObject.OnPropertyChanged -= OnPropertyChanged;
            propertyObject.OnPropertyChanged += OnPropertyChanged;
#endif
        }

        private void OnPropertyChanged()
        {
            if (!isActiveAndEnabled)
                return;
            
            try
            {
                ApplyPropertyObject(_propertyObject);
            }  
            catch (Exception e) when (e is MissingReferenceException || 
                                      e is MissingComponentException || 
                                      e is UnassignedReferenceException)
            {                
                _component = GetComponent<TComponent>();
                ApplyPropertyObject(_propertyObject);
            }
        }
        
        private void ApplyPropertyObject(ScriptableObject propertyObject)
        {
            if (propertyObject == null)
                return;
            
            SetProperty(GetPropertyFromObject(propertyObject));
        }
    }  
}
