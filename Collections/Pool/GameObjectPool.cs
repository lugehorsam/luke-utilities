using UnityEngine;

namespace Utilities
{
    public class GameObjectPool<T> : ObjectPool<T> where T : IGameObject, new() {

        public GameObjectPool(Prefab prefab,
            int initialSize,
            bool allowResize = true) : base(() =>
            {
                T instance = new T();
                instance.GameObject.SetActive(false);
                return instance;
            },
            initialSize,
            allowResize)
        {

        }

        protected sealed override void HandleOnRelease(T objectToRelease)
        {
            objectToRelease.GameObject.SetActive(true);
            HandleAfterEnabled(objectToRelease);
        }

        protected virtual void HandleAfterDisabled(T disabledObject)
        {
        }

        protected virtual void HandleAfterEnabled(T enabledObject)
        {
        }

        protected override void HandleOnPool(T objectToPool)
        {
            objectToPool.GameObject.SetActive(false);
            HandleAfterDisabled(objectToPool);
        }
    }
}
