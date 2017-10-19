using UnityEngine;

namespace Utilities.Bindings
{
    public class RotationProperty : Vector3Property<Transform>
    {
        Vector3? trackedRotation; //single rotation representation for conversion to quaternions

        protected override Vector3 Get(Transform component)
        {
            return trackedRotation.Value;
        }

        protected sealed override void Set(Transform component, Vector3 rot)
        {
            trackedRotation = rot;
            component.localEulerAngles = trackedRotation.Value;
        }
    }
}