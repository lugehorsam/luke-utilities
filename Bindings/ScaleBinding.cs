using UnityEngine;

namespace Utilities
{
    [System.Serializable]
    public class ScaleBinding : Vector3Binding<Transform> {
  
        public ScaleBinding(Transform transform) : base(transform)
        {        
        }
    
        public override Vector3 GetProperty ()
        {
            return Component.localScale;
        }

        public override void SetProperty (Vector3 property)
        {
            Component.localScale = property;
        }
    }   
}
