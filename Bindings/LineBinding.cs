using UnityEngine;

namespace Utilities
{
    public class LineBinding : Vector3Binding<LineRenderer>
    {       
        public LineBinding(LineRenderer lineRenderer
        ) : base(lineRenderer)
        {
        }

        public void SetInitialProperty(Vector3 position)
        {
            Component.SetPosition(0, position);    
        }
        
        public override void SetProperty(Vector3 position)
        {
            Component.SetPosition(Component.positionCount -1, position);
        }

        public void SetPropertyPermanent(Vector3 position)
        {
            SetProperty(position);            
            Component.positionCount++;
        }

        public override Vector3 GetProperty()
        {
            return Component.GetPosition(Component.positionCount - 1);
        }
        
        public Vector3 GetProperty(int index)
        {
            return Component.GetPosition(index);
        }

        public void Clear()
        {
            Component.SetPositions(new Vector3[]{Vector3.zero, Vector3.zero});
        }                
    }    
}
