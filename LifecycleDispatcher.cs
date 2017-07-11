using System;
using UnityEngine;

public class LifecycleDispatcher : MonoBehaviour
{
    public event Action OnLateUpdate = () => { };

    public event Action OnUpdate = () => { };

    void Update()
    {
        HandleUpdate();
        OnUpdate();
    }

    void LateUpdate()
    {
        OnLateUpdate();
    }

    protected virtual void HandleUpdate()
    {
    }

}
