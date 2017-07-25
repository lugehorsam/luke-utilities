using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Lerp<TProperty>
    where TProperty : struct {

    public float TargetDuration => targetDuration;

    [SerializeField]
    float targetDuration = 1f;

    public float CurrentTime => currentTime;

    float currentTime;

    bool instant = false;

    public bool HasReachedTargetTime
    {  
        get {
            if (currentDirection == LerpDirection.Forwards) {
                return CurrentTime >= targetDuration;
            } else {
                return CurrentTime <= 0f;
            }
        }
    }

    public TweenType TweenType => tweenType;

    [SerializeField]
    TweenType tweenType;

    public TProperty TargetProperty => targetProperty;

    [SerializeField]
    TProperty targetProperty;

    LerpDirection currentDirection = LerpDirection.Forwards;

    public void UpdateTime ()
    {
        if (currentDirection == LerpDirection.Forwards) {
            if (instant) {
                currentTime = targetDuration;
            } else {
                currentTime += Time.deltaTime;
            }
        } else if (currentDirection == LerpDirection.Backwards) {
            if (instant) {
                currentTime = 0f;
            } else {
                currentTime -= Time.deltaTime;
            }
        }
    }

    public Lerp (TProperty targetProperty, float targetDuration)
    {
        this.targetProperty = targetProperty;
        this.targetDuration = targetDuration;
    }

    public TProperty GetLerpedProperty (TProperty startProperty, Func<TProperty, TProperty, float, TProperty> lerpDelegate)
    {
        Func<float, float> easing = TweenType.TweenTypeToFunction ();
        float scaledTime = easing (CurrentTime / TargetDuration);
        TProperty lerpedValue = lerpDelegate (startProperty, TargetProperty, scaledTime);
        return lerpedValue;
    }
}
