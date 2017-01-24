using UnityEngine;
using System;

public class GameObjectPool<T> : ObjectPool<T> where T : Component {

    public GameObjectPool(Prefab prefab,
                          int initialSize,
                          bool allowResize = true) : base(() =>
                {
                    T instance = prefab.Instantiate<T>();
                    instance.gameObject.SetActive(false);
                    return instance;
                },
                initialSize,
                allowResize)
    {

    }

    protected sealed override void HandleOnRelease(T objectToRelease)
    {
        objectToRelease.gameObject.SetActive(true);
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
        objectToPool.gameObject.SetActive(false);
        HandleAfterDisabled(objectToPool);
    }
}
