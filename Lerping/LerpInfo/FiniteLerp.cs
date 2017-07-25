using System;
using UnityEngine;

namespace Utilities
{
    public class FiniteLerp<TProperty>
    {
        public float TargetDuration => _targetDuration;

        float _targetDuration = 1f;
    
        public float CurrentTime => currentTime;

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
    
        public TProperty TargetProperty => _targetProperty;

        TProperty _targetProperty;
    
        LerpDirection currentDirection = LerpDirection.Forwards;
        
        public FiniteLerp(TProperty targetProperty, float targetDuration, TweenType tweenType)
        {
            _targetProperty = targetProperty;
            _targetDuration = targetDuration;
            _easing = tweenType.TweenTypeToFunction();
        }
    
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
            float scaledTime = _easing(CurrentTime / TargetDuration);
            TProperty lerpedValue = lerpDelegate(startProperty, TargetProperty, scaledTime);
            return lerpedValue;
        }
    
        public FiniteLerp(TProperty targetProperty, float targetDuration, Func<float, float> easing)
        {
            _targetProperty = targetProperty;
            _targetDuration = targetDuration;
            _easing = easing;
        }
    }
}