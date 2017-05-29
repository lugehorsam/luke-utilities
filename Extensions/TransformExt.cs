using UnityEngine;
using System.Collections;

public static class TransformExt {

    public static void IncrementY (this Transform thisTransform, float yOffset)
    {
        thisTransform.localPosition = new Vector3 (thisTransform.localPosition.x, thisTransform.localPosition.y + yOffset, thisTransform.localPosition.z);
    }
    
    public static void SetX (this Transform thisTransform, float x)
    {
        thisTransform.localPosition = new Vector3 (x, thisTransform.localPosition.y, thisTransform.localPosition.z);
    }
    
    public static void SetY (this Transform thisTransform, float y)
    {
        thisTransform.localPosition = new Vector3 (thisTransform.localPosition.x,y, thisTransform.localPosition.z);
    }
    
    public static void SetZ (this Transform thisTransform, float z)
    {
        thisTransform.localPosition = new Vector3 (thisTransform.localPosition.x, thisTransform.localPosition.y, z);
    }
}
