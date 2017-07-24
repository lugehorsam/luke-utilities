
namespace Utilities
{
    using UnityEngine;
 
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
    
        public static void ResetLocalValues( this Transform t ) {
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
        }
    
        public static void ResetLocalValues( this RectTransform t ) {
            t.anchoredPosition = Vector2.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            t.sizeDelta = Vector2.zero;
        }
    
        /// <summary>
        /// In world space
        /// </summary>
        /// <param name="t"></param>
        public static Vector3 CenterWorldSpace( this RectTransform t )
        {
            var pos = VectorExt.Add(t.position, new Vector3(t.rect.width/2, -t.rect.height/2, 0));
            Debug.Log("t. position " + t.position + " rect size " + t.rect.size);
           

            return pos;
        }
    }
}
