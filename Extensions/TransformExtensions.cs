using UnityEngine;
using System.Collections;

public static class TransformExtensions {

    public static void IncrementY (this Transform thisTransform, float yOffset)
    {
        thisTransform.localPosition = new Vector3 (thisTransform.localPosition.x, thisTransform.localPosition.y + yOffset, thisTransform.localPosition.z);
    }
}
