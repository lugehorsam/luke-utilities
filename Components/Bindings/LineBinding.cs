using System.Linq;
using UnityEngine;

namespace Utilities
{
    public class LineBinding : Vector3Binding<LineRenderer>
    {
       
        public LineBinding(
            GameObject gameObject, LineRenderer lineRenderer
        ) : base(gameObject, lineRenderer)
        {
        }    
    
        public override void SetProperty(Vector3 position)
        {
            Component.SetPosition(Component.positionCount -1, position);
        }

        public override Vector3 GetProperty()
        {
            return Component.GetPosition(Component.positionCount - 1);
        }       
        
        
    }    
}
