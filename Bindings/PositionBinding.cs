using UnityEngine;

namespace Utilities
{
 
    public class PositionBinding : Vector3Binding<Transform> {

        public PositionBinding(Transform transform) : base(transform)
        {        
        }
    
        public PositionSpace PositionSpace 
        {
            get {
                return positionSpace;
            }
            set {
                positionSpace = value;
            }
        }

        [SerializeField]
        PositionSpace positionSpace = PositionSpace.LocalPosition;

        public override void SetProperty(Vector3 position) {
            if (positionSpace == PositionSpace.LocalPosition) {
                Component.localPosition = position;
            } else {
                Component.position = position;
            }
        }
  
        public override Vector3 GetProperty() {
            if (positionSpace == PositionSpace.LocalPosition) {
                return  Component.localPosition;
            } else {
                return  Component.position;
            }
        }
    }   

}
