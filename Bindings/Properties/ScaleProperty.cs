namespace Utilities.Bindings
{
    using UnityEngine;
    
    [System.Serializable]
    public class ScaleProperty : Vector3Property<Transform> 
    {
        protected override Vector3 Get (Transform component)
        {
            return component.localScale;
        }

        protected override void Set (Transform component, Vector3 property)
        {
            component.localScale = property;
        }
    }   
}
