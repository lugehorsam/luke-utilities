namespace Utilities.Bindings
{
    using System;
    using UnityEngine;

    public class PropertyObject<T> : ScriptableObject
    {        
#if UNITY_EDITOR
        public event Action OnPropertyChanged = delegate { };
#endif
            
        [SerializeField] private T _property;
        
        public T Property => ProcessProperty(_property);

        protected virtual T ProcessProperty(T property)
        {
            return property;
        }

        private void OnValidate()
        {
            OnPropertyChanged();
        }
    }    
}
