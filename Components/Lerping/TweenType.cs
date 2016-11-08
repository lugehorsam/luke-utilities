using System;
using DigitalRuby.Tween;

public enum TweenType
{
    Linear,
    CubicEaseOut
}

public static class TweenTypeExtensions
{
    public static Func<float, float> TweenTypeToFunction (this TweenType tweenType)
    {
        switch (tweenType) {
            case TweenType.CubicEaseOut:
                return TweenScaleFunctions.CubicEaseOut;
            case TweenType.Linear:
            default:
                return TweenScaleFunctions.Linear;
        }
    }
}