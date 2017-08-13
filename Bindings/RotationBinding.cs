using UnityEngine;

namespace Utilities
{ 
    public class RotationBinding : Vector3Binding<Transform> {

        Vector3? trackedRotation; //single rotation representation for conversion to quaternions

        protected RotationBinding(Transform transform) : base(transform)
        {        
            trackedRotation = Component.localEulerAngles;
        }

        public sealed override void SetProperty(Vector3 rot) 
        {
            trackedRotation = rot;
            Component.localEulerAngles = trackedRotation.Value;
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