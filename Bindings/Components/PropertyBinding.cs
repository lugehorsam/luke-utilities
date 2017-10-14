using System.Linq;

namespace Utilities.Bindings
{
    using System;
    using UnityEngine;

    [ExecuteInEditMode]
    public abstract class PropertyBinding<TProperty, TComponent> : MonoBehaviour
        where TComponent : Component
    {
        [SerializeField] private string _variantId;
        
        [SerializeField] private ScriptableObject _propertyObject;
        [SerializeField] private PropertyObjectVariant[] _propertyObjectVariants;
        
        private TComponent _component;

        protected TComponent _Component => _component ?? (_component = GetComponent<TComponent>());

        public abstract TProperty GetProperty();
        public abstract void SetProperty(TProperty property);

        protected PropertyObject<T> TryCastPropertyObject<T>(ScriptableObject propertyObject)
        {
            if (_propertyObject == null)
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
            TryApplyProperty();
        }

        private void OnValidate()
        {
            _propertyObject = _propertyObject ?? AssignObjectFromVariants();
            TryApplyProperty();

#if UNITY_EDITOR
            AddSubPropertyListeners();
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
            _propertyObject = _propertyObject ?? AssignObjectFromVariants();
            
            if (_propertyObject == null)
            {
                Diag.Warn($"Could not find property object on {this}");
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

        private ScriptableObject AssignObjectFromVariants()
        {
            return _propertyObjectVariants.FirstOrDefault(obj => obj.Id == _variantId).Object;
        }
    }  
}
