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
}
