using UnityEngine;

namespace Utilities.Bindings
{ 
    public class RotationComponent : Vector3Property<Transform> 
    {
        Vector3? trackedRotation; //single rotation representation for conversion to quaternions

        protected override Vector3 Get(Transform component)
        {
            return trackedRotation.Value;
        }

        protected override void Set(Transform component, Vector3 property)
        {
            trackedRotation = property;
            component.localEulerAngles = trackedRotation.Value;
        }
    }   
}