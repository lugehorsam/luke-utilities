using System;
using System.Collections;
using UnityEngine;

namespace Utilities
{
    public abstract class LerpBinding<TProperty, TComponent> : PropertyBinding<TProperty, TComponent>
        where TComponent : Component 
    {

        public event Action<LerpBinding<TProperty, TComponent>> OnLerp = (binding) => { };

        protected LerpBinding(GameObject gameObject, TComponent component) : base(gameObject, component)
        {
        }

        public abstract Func<TProperty, TProperty, TProperty> GetRandomizeDelegate ();
        public abstract TProperty AddProperty (TProperty property1, TProperty property2);

        public void IncrementProperty (TProperty incrementValue)
        {
            SetProperty (AddProperty (GetProperty (), incrementValue));
        }

        public void AddPropertyRandomly (TProperty min, TProperty max)
        {
            TProperty randValue = GetRandomizeDelegate () (min, max);
            TProperty newValue = AddProperty (GetProperty (), randValue);
            SetProperty (newValue);
        }

        public void SetPropertyRandomly (TProperty min, TProperty max)
        {
            TProperty randValue = GetRandomizeDelegate () (min, max);
            SetProperty (randValue);
        }

        protected abstract Func<TProperty, TProperty, float, TProperty> GetLerpDelegate ();


        public IEnumerator CreateLerpEnumerator (FiniteLerp<TProperty> lerp)
        {
            while (!lerp.HasReachedTargetTime) {
                TProperty lerpedProperty = lerp.GetLerpedProperty (GetProperty (), GetLerpDelegate());
                SetProperty (lerpedProperty);
                lerp.UpdateTime ();
                OnLerp (this);
                yield return null;
            }
        }

        IEnumerator ApplyInfiniteLerp(InfiniteLerp<TProperty> lerp)
        {
            while (true)
            {
                IncrementProperty(lerp.UnitsPerSecond);
                OnLerp(this);
                yield return null;
            }
        }       
    }
}
