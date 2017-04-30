using System;
using UnityEngine;

public class LifecycleDispatcher : MonoBehaviour
{
    public event Action OnUpdate = () => { };

    void Update()
    {
        HandleUpdate();
        OnUpdate();
    }

    protected virtual void HandleUpdate()
    {
    }

}
