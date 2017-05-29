using UnityEngine;
using System;

namespace Utilities
{
    [Serializable]
    public class LazyPrefab : Prefab
    {
        private GameObject instance;

        public LazyPrefab(Transform holder, Vector3 pos) : base(holder, pos) {}
        public LazyPrefab(Transform holder) : base(holder) {}

        public sealed override T Instantiate<T>()
        {
            instance = instance ?? base.Instantiate<T>().gameObject;
            return instance.GetOrAddComponent<T>();
        }

        public override GameObject Instantiate()
        {
            instance = instance ?? base.Instantiate();
            return instance;
        }
    }
}
