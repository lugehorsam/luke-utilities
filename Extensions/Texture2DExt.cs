namespace Utilities
{
    using System;

    using UnityEngine;

    public static class Texture2DExt
    {
        public static Texture2D ScaleTexture(this Texture2D thisTexture, int targetWidth, int targetHeight)
        {
            throw new NotImplementedException();

            /**var result = new Texture2D(targetWidth, targetHeight, thisTexture.format, false);
            float incX = 1.0f / targetWidth;
            float incY = 1.0f / targetHeight;
            for (var i = 0; i < result.height; ++i)
            {
                for (var j = 0; j < result.width; ++j)
                {
                    Color newColor = thisTexture.GetPixelBilinear(j / (float) result.width, i / (float) result.height);
                    result.SetPixel(j, i, newColor);
                }
            }

            result.Apply();
            return result;**/
        }
    }
}
