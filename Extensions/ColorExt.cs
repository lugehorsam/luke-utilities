﻿namespace Utilities
{
    using UnityEngine;

    public static class ColorExt
    {
        private const float _RGB_BASE = 255f;

        public static Color FromRGB(float red, float green, float blue)
        {
            return new Color(red / _RGB_BASE, green / _RGB_BASE, blue / _RGB_BASE);
        }

        public static Color SetAlpha(this Color thisColor, float alpha)
        {
            return new Color(thisColor.r, thisColor.g, thisColor.b, alpha);
        }

        public static bool ApproximatelyEquals(this Color color, Color otherColor)
        {
            return (color.r == otherColor.r) && (color.g == otherColor.g) && (color.b == otherColor.b) && (color.a == otherColor.a);
        }
    }
}
