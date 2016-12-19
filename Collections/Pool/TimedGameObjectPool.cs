using UnityEngine;
using System.Collections;

public class TimedGameObjectPool<T> : GameObjectPool<T> where T : Component, ITimedBehavior {

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
