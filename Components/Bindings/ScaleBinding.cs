using UnityEngine;
using System.Collections;

[System.Serializable]
public class ScaleBinding : Vector3Binding<Transform> {
  
    protected ScaleBinding(MonoBehaviour coroutineRunner, GameObject gameObject) : base(coroutineRunner, gameObject)
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
