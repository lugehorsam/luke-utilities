using UnityEngine;

namespace Utilities
{ 
    public class ColliderResizer 
    {  
        private readonly BoxCollider boxCollider;
        private readonly MeshRenderer meshRenderer;
    
        private readonly Vector3 _padding;

        public ColliderResizer(GameObject gameObject, Mesh mesh, Vector3 padding = default(Vector3))
        {
            boxCollider = gameObject.GetComponent<BoxCollider>();
         
        }     

        public void Resize () 
        {
            Vector3 paddedSize = meshRenderer.GetScaledSize();
            paddedSize.x += _padding.x;
            paddedSize.y += _padding.y;
            paddedSize.z += _padding.z;
            boxCollider.size = paddedSize;            
        }
    }   
}
