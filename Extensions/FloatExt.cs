namespace Utilities
{
    using System;

    using UnityEngine;

    public static class FloatExt
    {
        public static bool ApproximatelyEquals(this float thisFloat, float otherFloat, float differenceCap)
        {
            float difference = Math.Abs(thisFloat - otherFloat);
            float sign1 = Mathf.Sign(thisFloat);
            float sign2 = Mathf.Sign(otherFloat);
            return (sign1 == sign2) && (difference < differenceCap);
        }

        public static bool ApproximatelyLessThan(this float thisFloat, float otherFloat, float differenceCap)
        {
            return (thisFloat < otherFloat) || thisFloat.ApproximatelyEquals(otherFloat, differenceCap);
        }

        public static bool ApproximatelyGreaterThan(this float thisFloat, float otherFloat, float differenceCap)
        {
            return (thisFloat > otherFloat) || thisFloat.ApproximatelyEquals(otherFloat, differenceCap);
        }
    }
}
