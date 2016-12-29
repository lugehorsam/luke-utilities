using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatExtensions {

    public static bool ApproximatelyEquals(this float thisFloat, float otherFloat, float differenceCap)
    {
        return Math.Sign(thisFloat) == Math.Sign(otherFloat) &&
               Math.Abs(thisFloat - otherFloat) < differenceCap;
    }

    public static bool ApproximatelyLessThan(this float thisFloat, float otherFloat, float differenceCap)
    {
        return thisFloat < otherFloat || thisFloat.ApproximatelyEquals(otherFloat, differenceCap);
    }

    public static bool ApproximatelyGreaterThan(this float thisFloat, float otherFloat, float differenceCap)
    {
        return thisFloat > otherFloat || thisFloat.ApproximatelyEquals(otherFloat, differenceCap);
    }
}
