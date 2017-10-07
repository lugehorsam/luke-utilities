namespace Utilities.Bindings
{
    using UnityEngine;

    public class BindingObject<T> : ScriptableObject
    {
        [SerializeField] private T _property;
    }    
}
