using UnityEngine;

namespace Utilities.Bindings
{
    [ExecuteInEditMode]
    public class PositionBinding : Vector3Binding<Transform>
    {               
        public PositionSpace PositionSpace
        {
            get 
            {
                return _positionSpace;
            }
            set 
            {
                _positionSpace = value;
            }
        }

        [SerializeField] PositionSpace _positionSpace = PositionSpace.Local;

        public override void SetProperty(Vector3 position) 
        {            
            if (_positionSpace == PositionSpace.Local) 
            {
                _Component.localPosition = position;
            } 
            else 
            {
                _Component.position = position;
            }
        }
  
        public override Vector3 GetProperty() 
        {
            if (_positionSpace == PositionSpace.Local) 
            {
                return _Component.localPosition;
            } 

            return _Component.position;
        }
    }   

}
