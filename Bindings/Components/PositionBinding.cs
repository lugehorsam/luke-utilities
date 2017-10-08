using UnityEngine;

namespace Utilities.Bindings
{
    public class PositionBinding : Vector3Binding<Transform> 
    {    
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
                _Component.localPosition = position;
            } else {
                _Component.position = position;
            }
        }
  
        public override Vector3 GetProperty() {
            if (positionSpace == PositionSpace.LocalPosition) {
                return  _Component.localPosition;
            } else {
                return  _Component.position;
            }
        }
    }   

}
