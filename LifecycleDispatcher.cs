using System;
using UnityEngine;

public class LifecycleDispatcher : MonoBehaviour
{
    public event Action OnUpdate = () => { };

    void Update()
    {
        OnUpdate();
    }

}
