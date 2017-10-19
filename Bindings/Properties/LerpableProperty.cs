using System;
using System.Collections;
using UnityEngine;

namespace Utilities.Bindings
{
    public abstract class LerpableProperty<T, K> : Property<T, K>
        where K : Component
    {
        public event Action<LerpableProperty<T, K>> OnLerp = binding => { };

        public abstract Func<T, T, T> GetRandomizeDelegate ();
        public abstract T Add (T property1, T property2);

        public void IncrementProperty (K component, T incrementValue)
        {
            Set (component, Add (Get (component), incrementValue));
        }

        public void AddPropertyRandomly (K component, T min, T max)
        {
            T randValue = GetRandomizeDelegate () (min, max);
            T newValue = Add (Get (component), randValue);
            Set (component, newValue);
        }

        public void SetPropertyRandomly (K component, T min, T max)
        {
            T randValue = GetRandomizeDelegate () (min, max);
            Set (component, randValue);
        }

        protected abstract Func<T, T, float, T> GetLerpDelegate ();

        public IEnumerator CreateLerpEnumerator 
        (
            K component,
            FiniteLerp<T> lerp
        )
        {
            T initialProperty = Get(component);
            
            while (!lerp.HasReachedTargetTime) {
                T lerpedProperty = lerp.GetLerpedProperty (initialProperty, GetLerpDelegate());
                Set (component, lerpedProperty);
                lerp.UpdateTime ();
                OnLerp (this);
                yield return null;
            }
        }         
    }
}
