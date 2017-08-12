
namespace Utilities
{
    using UnityEngine;
    using System.Collections.Generic;
 
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
    
        public static void ResetLocalValues(this Transform t) {
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
        }

        public static void DestroyChildren(this Transform transform)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject.Destroy(transform.GetChild(i));
            }
        }

        public static List<Transform> GetChildren(this Transform transform)
        {
            var children = new List<Transform>();
            
            for (int i = 0; i < transform.childCount; i++)
            {
               children.Add(transform.GetChild(i));
            }
            
            return children;
        }
    }
}
