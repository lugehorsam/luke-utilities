namespace Utilities.Bindings
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class PropertyObject<T> : ScriptableObject, IPropertyObject
    {        
#if UNITY_EDITOR
        public event Action OnPropertyChanged = delegate { };
#endif

        protected virtual IEnumerable<IPropertyObject> ObjectsToWatch => null;
            
        [SerializeField] private T _property;
        
        public T Property => ProcessProperty(_property);

        protected virtual T ProcessProperty(T property)
        {
            return property;
        }

        private void OnValidate()
        {
            OnPropertyChanged();
            
            if (ObjectsToWatch != null)
            {
                foreach (var subObject in ObjectsToWatch)
                {
                    subObject.OnPropertyChanged -= OnValidate;
                    subObject.OnPropertyChanged += OnValidate;
                }                
            }
        }
    }    
}
