using UnityEngine;
using System.Collections;

public static class MeshRendererExtensions {
    public static Vector2 GetScaledSize(this MeshRenderer thisRenderer) {	        
        Vector3 size = thisRenderer.bounds.size;
        Vector3 scale = thisRenderer.transform.localScale;
        return new Vector2 (size.x / scale.x, size.y / scale.y);
    }
}
