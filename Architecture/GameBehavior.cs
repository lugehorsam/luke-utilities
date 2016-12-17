using UnityEngine;
using System.Collections;

public class GameBehavior : MonoBehaviour {
     void Awake()
    {
        InitComponents();
        InitObjects();
        AddEventHandlers();
        OnAwake();
    }

    protected virtual void InitComponents()
    {
    }

    protected virtual void AddEventHandlers()
    {

    }

    protected virtual void RemoveEventHandlers()
    {

    }

    protected virtual void OnAwake()
    {
    }

    protected virtual void InitObjects()
    {
    }

    void Destroy()
    {
        RemoveEventHandlers();
    }
}
