using UnityEngine;
using System.Collections;

[System.Serializable]
public class ScaleBinding : Vector3Binding<Transform> {
  
    public override Vector3 GetProperty ()
    {
        return transform.localScale;
    }

    public override void SetProperty (Vector3 property)
    {
        transform.localScale = property;
    }
}
