using UnityEngine;

namespace Utilities
{
    [System.Serializable]
    public class ScaleBinding : Vector3Binding<Transform> {
  
        public ScaleBinding(GameObject gameObject) : base(gameObject, gameObject.transform)
        {        
        }
    
        public override Vector3 GetProperty ()
        {
            return GameObject.transform.localScale;
        }

        public override void SetProperty (Vector3 property)
        {
            GameObject.transform.localScale = property;
        }
    }   
}
