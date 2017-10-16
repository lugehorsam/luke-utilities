using UnityEditor;

namespace Utilities.Bindings
{
    using System;
    using UnityEngine;

    [ExecuteInEditMode]
    public abstract class PropertyComponent<TProperty, TComponent> : MonoBehaviour, IPropertyComponent
        where TComponent : Component
    {
        [SerializeField] private PropertyObject _propertyObject;
        [SerializeField] private bool _expectingVariant;
        
        private TComponent _component;

        protected TComponent _Component => _component ?? (_component = GetComponent<TComponent>());

        public abstract TProperty GetProperty();
        public abstract void SetProperty(TProperty property);
        public abstract BindType BindType { get; }
        
        public void SetPropertyObject(PropertyObject propertyObject)
        {
            _propertyObject = propertyObject;
            TryApplyProperty();
        }

        private PropertyObject<T> TryCastPropertyObject<T>(ScriptableObject propertyObject)
        {
            if (propertyObject == null)
                return null;
            
            PropertyObject<T> castedPropertyObject = propertyObject as PropertyObject<T>;
            
            if (castedPropertyObject == null)
                throw new NullReferenceException($"Object {propertyObject} on binding {this} could not be casted to a {typeof(PropertyObject<T>)}.");

            return castedPropertyObject;
        }

        private TProperty GetPropertyFromObject(ScriptableObject propertyObject)
        {
            var castedPropertyObject = TryCastPropertyObject<TProperty>(propertyObject);
            return castedPropertyObject.Property;
        }
        
        private void Awake()
        {
            if (_expectingVariant)
                return;
            
            TryApplyProperty();
        }

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlayingOrWillChangePlaymode)
            {
                TryApplyProperty();
                AddSubPropertyListeners();
            }
#endif
        }

#if UNITY_EDITOR
        private void AddSubPropertyListeners()
        {
            PropertyObject<TProperty> propertyObject = TryCastPropertyObject<TProperty>(_propertyObject);

            if (propertyObject == null)
                return;
            
            propertyObject.OnPropertyChanged -= TryApplyProperty;
            propertyObject.OnPropertyChanged += TryApplyProperty;
        }
#endif
        
        private void TryApplyProperty()
        {            
            if (_propertyObject == null)
            {
                Diag.Warn($"Could not find property object on {this} with root {transform.root.gameObject.name}");
                return;
            }
            
            ApplyPropertyObject(_propertyObject);
        }
        
        private void ApplyPropertyObject(ScriptableObject propertyObject)
        {
            if (propertyObject == null)
                return;
            
            SetProperty(GetPropertyFromObject(propertyObject));
        }
    }  
}
