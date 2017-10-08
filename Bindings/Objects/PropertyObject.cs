namespace Utilities.Bindings
{
    using UnityEngine;

    public class PropertyObject<T> : ScriptableObject
    {
        [SerializeField] private T _property;
        public T Property => _property;
    }    
}
