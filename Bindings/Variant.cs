namespace Utilities.Bindings
{
    using UnityEngine;

    [CreateAssetMenu]
    public class Variant : ScriptableObject
    {
        [SerializeField] private Prefab _prefab;
        [SerializeField] private PropertyObject[] _propertyObjects;

        public GameObject Instantiate(Transform parent)
        {
            var prefab = _prefab.Instantiate(parent);
            
            var propertyComponents = prefab.GetComponentsWithInterface<IPropertyComponent>();
            
            foreach (var propertyObject in _propertyObjects)
            {
                foreach (var propertyComponent in propertyComponents)
                {
                    Diag.Log("obj and comp " + propertyObject.BindType + ", " + propertyComponent.BindType);
                    
                    if (propertyComponent.BindType == propertyObject.BindType)
                    {
                        propertyComponent.SetPropertyObject(propertyObject);
                    }
                }
            }
			
            return prefab;
        }
    }
}
