using System;
using UnityEngine;

namespace Utilities.Bindings
{
    public abstract class Vector3Component<TComponent> : LerpComponent<Vector3, TComponent>
        where TComponent : Component {

        protected sealed override Func<Vector3, Vector3, float, Vector3> GetLerpDelegate() 
        {
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
