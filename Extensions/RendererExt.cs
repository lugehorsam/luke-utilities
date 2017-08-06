using UnityEngine;

namespace Utilities
{
    public static class RendererExt {
        public static float Width (this Renderer renderer)
        {
            return renderer.bounds.size.x;
        }

        public static float Height (this Renderer renderer)
        {
            return renderer.bounds.size.x;
        }

        public static void CenterToParentRenderer (this Renderer childRenderer, Renderer parentRenderer)
        {
            if (childRenderer.transform.IsChildOf (parentRenderer.transform)) {
                Vector3 childCenter = childRenderer.bounds.center;
                Vector3 parentCenter = parentRenderer.bounds.center;
                Vector3 offset = parentCenter - childCenter;
                childRenderer.transform.position += offset;
            } else {
                Diag.Report ("child renderer " + childRenderer + " is not a child of " + parentRenderer);
            }
       
        }
    }   
}
