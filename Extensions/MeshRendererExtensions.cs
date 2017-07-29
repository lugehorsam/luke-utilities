using UnityEngine;

namespace Utilities
{

    public static class MeshRendererExtensions {
    
        /// <summary>
        /// Gets the bounds of the mesh, applying scale to the bounds.
        /// </summary>
        public static Vector3 GetScaledSize(this MeshRenderer thisRenderer) 
        {	        
            Vector3 size = thisRenderer.bounds.size;
            Vector3 scale = thisRenderer.transform.localScale;
            return new Vector3 (size.x / scale.x, size.y / scale.y, size.z / scale.z);
        }
    }    
}
