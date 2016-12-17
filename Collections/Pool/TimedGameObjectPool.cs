using UnityEngine;
using System.Collections;

public class TimedGameObjectPool<T> : GameObjectPool<T> where T : Component, ITimedBehavior {

    public TimedGameObjectPool(Prefab prefab,
                               int initialSize,
                               bool allowResize = true)
        : base(prefab,
               initialSize,
               allowResize)
    {

    }


    protected sealed override void HandleAfterEnabled(T enabledObject)
    {
        enabledObject.TimedBehavior.ResetTimer();
        enabledObject.TimedBehavior.OnExpire += HandleExpiredGameObject;
    }

    protected override void HandleAfterDisabled(T disabledObject)
    {
        Pool(disabledObject.GetComponent<T>());
        disabledObject.OnExpire -= HandleExpiredGameObject;
    }


    void HandleExpiration(T expiredObject)
    {

    }
}
