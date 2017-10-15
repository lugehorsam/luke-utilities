using UnityEngine;

namespace Utilities.Bindings
{
    [System.Serializable]
    public class ScaleComponent : Vector3Component<Transform> 
    {
        public override BindType BindType => BindType.Scale;

        public override Vector3 GetProperty ()
        {
            return _Component.localScale;
        }

        public override void SetProperty (Vector3 property)
        {
            _Component.localScale = property;
        }
    }   
}
