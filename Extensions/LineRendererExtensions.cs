using UnityEngine;

namespace Utilities
{
    
    public static class LineRendererExtensions {

        public static void SetWidth(this LineRenderer thisLineRenderer, float width)
        {
            thisLineRenderer.startWidth = thisLineRenderer.endWidth = width;
        }

        public static float GetWidth(this LineRenderer thisLineRenderer)
        {
            float start = thisLineRenderer.startWidth;
            float end = thisLineRenderer.endWidth;
            
            if (start != end)
            {
                Diagnostics.LogWarning("Trying to interpolate widths of line renderer that are not equal.");
            }

            return (start + end) / 2;
        }
    }
}
