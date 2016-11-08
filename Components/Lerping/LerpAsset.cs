using UnityEngine;
using System;

public class LerpAsset : ScriptableObject {

    [SerializeField]
    AnimationCurve curve;

    public static implicit operator Func<float, float>(LerpAsset asset)
    {
        return (xValue) => asset.curve.Evaluate(xValue);
    }
}
