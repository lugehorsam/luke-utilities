using System;
using DigitalRuby.Tween;

public enum TweenType
{
    Linear

}

public static class TweenTypeExtensions
{
    public static Func<float, float> TweenTypeToFunction (this TweenType tweenType)
    {
        switch (tweenType) {
            case TweenType.Linear:
            default:
                return TweenScaleFunctions.Linear;
        }
    }
}