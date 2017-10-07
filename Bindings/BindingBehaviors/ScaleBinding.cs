using UnityEngine;

namespace Utilities
{
    [System.Serializable]
    public class ScaleBinding : Vector3Binding<Transform> 
    {   
        public override Vector3 GetCurrentProperty ()
        {
            return _Component.localScale;
        }

        public override void SetProperty (Vector3 property)
        {
            _Component.localScale = property;
        }
    }   
}
