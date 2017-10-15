namespace Utilities.Bindings
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class PropertyObject<T> : PropertyObject
    {        
        [SerializeField] private T _property;
        
        public T Property
        {
            get
            {
                var processedProperty = ProcessProperty(_property);
                Diag.Log(UtilitiesFeature.Binding, $"Property object {this} is returning property {processedProperty}");
                return processedProperty;
            }
        }

        protected virtual T ProcessProperty(T property)
        {
            return property;
        }
    }

    public class PropertyObject : ScriptableObject
    {        
#if UNITY_EDITOR
            public event Action OnPropertyChanged;
#endif
            public BindType BindType { get; }
        
            protected virtual IEnumerable<PropertyObject> ObjectsToWatch => null;            


#if UNITY_EDITOR
        private void OnValidate()
        {
            OnPropertyChanged();
            SubscribeToObjects();
        }
#endif
        
        private void SubscribeToObjects()
        {
            if (ObjectsToWatch == null) return;
            
            foreach (var subObject in ObjectsToWatch)
            {
                if (subObject == null)
                    continue;
                    
                subObject.OnPropertyChanged -= OnValidate;
                subObject.OnPropertyChanged += OnValidate;
            }
        }

    }
}
