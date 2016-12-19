using UnityEngine;
using System.Collections;

public class GameBehavior : MonoBehaviour {

    protected virtual float InitialZ
    {
        get
        {
            return 0f;
        }
    }

     void Awake()
    {
        InitComponents();
        InitObjects();
        AddEventHandlers();
        transform.position.Set(0, 0, InitialZ);
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
