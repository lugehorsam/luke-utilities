using UnityEngine;

namespace Utilities.Bindings
{ 
    public class RotationComponent : Vector3Component<Transform> 
    {

        public override BindType BindType => BindType.Rotation;

        Vector3? trackedRotation; //single rotation representation for conversion to quaternions

        public sealed override void SetProperty(Vector3 rot) 
        {
            trackedRotation = rot;
            _Component.localEulerAngles = trackedRotation.Value;
        }

        public sealed override Vector3 GetProperty() 
        {
            return trackedRotation.Value;
        }

        public sealed override Vector3 AddProperty(Vector3 v1, Vector3 v2)
        {
            return base.AddProperty(v1, v2);
        }
    }   
}