using UnityEngine;

namespace Utilities
{
    public class TimedGameObjectPool<T> : GameObjectPool<T> where T : IGameObject, ITimedBehavior, new() {

        public TimedGameObjectPool(Prefab prefab,
            int initialSize,
            bool allowResize = true)
            : base(prefab,
                initialSize,
                allowResize) {}

        protected sealed override void HandleAfterEnabled(T enabledObject)
        {
            enabledObject.TimedBehavior.ResetTimer();
            enabledObject.TimedBehavior.OnExpire += HandleExpiration;
        }

        protected override void HandleAfterDisabled(T disabledObject)
        {
            disabledObject.TimedBehavior.OnExpire -= HandleExpiration;
        }

        void HandleExpiration(TimedBehavior expiredObject)
        {
            Pool(expiredObject.GetComponent<T>());
        }
    }
}
