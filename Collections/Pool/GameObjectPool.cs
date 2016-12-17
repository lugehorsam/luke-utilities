using UnityEngine;
using System.Collections;

public class GameObjectPool<T> : ObjectPool<T> where T : Component {

    public GameObjectPool(Prefab prefab,
                          int initialSize,
                          bool allowResize = true)
        : base(() => prefab.Instantiate<T>(),
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
