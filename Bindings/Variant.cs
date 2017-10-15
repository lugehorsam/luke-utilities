namespace Utilities.Bindings
{
    using UnityEngine;

    [CreateAssetMenu]
    public class Variant : ScriptableObject
    {
        [SerializeField] private Prefab _prefab;
        [SerializeField] private PropertyObject[] _propertyObjects;

        public GameObject Instantiate()
        {
            var instance = _prefab.Instantiate();

            foreach (var propertyObject in _propertyObjects)
            {
                foreach (var component in instance.GetComponentsWithInterface<IPropertyComponent>())
                {
                    if (component.BindType == propertyObject.BindType)
                    {
                        component.SetPropertyObject(propertyObject);
                    }
                }
            }
			
            return instance;
        }
    }
}
