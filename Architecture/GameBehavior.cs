using UnityEngine;
using System.Collections;

public class GameBehavior : MonoBehaviour {
     void Awake()
    {
        InitComponents();
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

    void Destroy()
    {
        RemoveEventHandlers();
    }
}
