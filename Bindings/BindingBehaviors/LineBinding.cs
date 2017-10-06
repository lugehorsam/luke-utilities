using UnityEngine;

namespace Utilities
{
    public class LineBinding : Vector3Binding<LineRenderer>
    {       
        public void SetInitialProperty(Vector3 position)
        {
            _Component.SetPosition(0, position);    
        }
        
        public override void SetProperty(Vector3 position)
        {
            _Component.SetPosition(_Component.positionCount -1, position);
        }

        public void SetPropertyPermanent(Vector3 position)
        {
            SetProperty(position);            
            _Component.positionCount++;
        }

        public override Vector3 GetProperty()
        {
            return _Component.GetPosition(_Component.positionCount - 1);
        }
        
        public Vector3 GetProperty(int index)
        {
            return _Component.GetPosition(index);
        }

        public void Clear()
        {
            _Component.SetPositions(new Vector3[]{Vector3.zero, Vector3.zero});
        }                
    }    
}
