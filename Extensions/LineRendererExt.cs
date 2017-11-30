namespace Utilities
{
    using UnityEngine;

    public static class LineRendererExt
    {
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
                Diag.Warn("Trying to interpolate widths of line renderer that are not equal.");
            }

            return (start + end) / 2;
        }
    }
}
