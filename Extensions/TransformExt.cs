namespace Utilities
{
    using System.Collections.Generic;

    using UnityEngine;

    public static class TransformExt
    {
        public static void IncrementY(this Transform thisTransform, float yOffset)
        {
            thisTransform.localPosition = new Vector3(thisTransform.localPosition.x,
                                                      thisTransform.localPosition.y + yOffset,
                                                      thisTransform.localPosition.z);
        }

        public static void SetX(this Transform thisTransform, float x)
        {
            thisTransform.localPosition = new Vector3(x, thisTransform.localPosition.y, thisTransform.localPosition.z);
        }

        public static void SetY(this Transform thisTransform, float y)
        {
            thisTransform.localPosition = new Vector3(thisTransform.localPosition.x, y, thisTransform.localPosition.z);
        }

        public static void SetZ(this Transform thisTransform, float z)
        {
            thisTransform.localPosition = new Vector3(thisTransform.localPosition.x, thisTransform.localPosition.y, z);
        }
        
        public static void ResetLocalValues(this Transform t)
        {
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
        }

        public static void DestroyChildren(this Transform transform)
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                Object.Destroy(transform.GetChild(i));
            }
        }

        public static List<T> GetChildren<T>(this T transform) where T : Transform
        {
            var children = new List<T>();

            for (var i = 0; i < transform.childCount; i++)
            {
                children.Add(transform.GetChild(i) as T);
            }

            return children;
        }

        public static Vector3 GetNewScale(this Transform thisTransform, float modifier)
        {
            return thisTransform.localScale.Map(dim => dim *= modifier);
        }
    }
}
