using UnityEngine;
using System;

public class FiniteLerp<TProperty>
    where TProperty : struct
{
	public float TargetDuration
    {
        get
        {
            return _targetDuration;
        }
    }

    float _targetDuration = 1f;

    public float CurrentTime
    {
        get
        {
            return currentTime;
        }
    }

    float currentTime;

    bool instant = false;

    public bool HasReachedTargetTime
    {
        get
        {
            if (currentDirection == LerpDirection.Forwards)
            {
                return CurrentTime >= _targetDuration;
            }
            else {
                return CurrentTime <= 0f;
            }
        }
    }

    Func<float, float> _easing;

    public TProperty TargetProperty
    {
        get
        {
            return _targetProperty;
        }
    }

    TProperty _targetProperty;

    LerpDirection currentDirection = LerpDirection.Forwards;

    public void UpdateTime()
    {
        if (currentDirection == LerpDirection.Forwards)
        {
            if (instant)
            {
                currentTime = _targetDuration;
            }
            else {
                currentTime += Time.deltaTime;
            }
        }
        else if (currentDirection == LerpDirection.Backwards)
        {
            if (instant)
            {
                currentTime = 0f;
            }
            else {
                currentTime -= Time.deltaTime;
            }
        }
    }

    public TProperty GetLerpedProperty(TProperty startProperty, Func<TProperty, TProperty, float, TProperty> lerpDelegate)
    {
        Debug.Log("Current time " + CurrentTime + "Target " + TargetDuration);
        float scaledTime = _easing(CurrentTime / TargetDuration);
        TProperty lerpedValue = lerpDelegate(startProperty, TargetProperty, scaledTime);
        return lerpedValue;
    }

    public FiniteLerp(TProperty targetProperty, float targetDuration, TweenType tweenType)
    {
        _targetProperty = targetProperty;
        _targetDuration = targetDuration;
        _easing = tweenType.TweenTypeToFunction();
    }

    public FiniteLerp(TProperty targetProperty, float targetDuration, Func<float, float> easing)
    {
        _targetProperty = targetProperty;
        _targetDuration = targetDuration;
        _easing = easing;
    }
}
