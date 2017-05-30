using System;
using UnityEngine;

namespace Utilities
{
    [Serializable]
    public abstract class Vector3Binding<TComponent> : LerpBinding<Vector3, TComponent>
        where TComponent : Component {

        protected Vector3Binding(MonoBehaviour coroutineRunner, GameObject gameObject, TComponent component) : base(coroutineRunner, gameObject, component)
        {        
        }

        protected sealed override Func<Vector3, Vector3, float, Vector3> GetLerpDelegate() {
            return Vector3.Lerp;
        }

        public override Vector3 AddProperty(Vector3 v1, Vector3 v2) {

            return v1 + v2;
        }

        public sealed override Func<Vector3, Vector3, Vector3> GetRandomizeDelegate ()
        {
            return (v1, v2) => Vector3.Lerp(v1, v2, UnityEngine.Random.value);
        }
    }
}
