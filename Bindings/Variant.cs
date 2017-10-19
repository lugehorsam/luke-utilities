namespace Utilities.Bindings
{
    using UnityEngine;

    [CreateAssetMenu]
    public class Variant : ScriptableObject
    {
        [SerializeField] private Prefab _prefab;
        [SerializeField] private Property[] _properties;
        
        public virtual GameObject Instantiate(Transform parent)
        {
            var instance = _prefab.Instantiate(parent);
            
            foreach (var property in _properties)
            {
                property.Init(instance);
            }

            return instance;
        }
    }
}
