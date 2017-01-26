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

    public bool Initialized
    {
        get
        {
            return initialized;
        }
    }
    private bool initialized = false;

     void Awake()
    {
        if (!initialized)
        {
            Init();
        }
    }

    public void Init()
    {
        initialized = true;
        InitComponents();
        InitObjects();
        AddEventHandlers();
        transform.position.Set(0, 0, InitialZ);
        OnInitialized();
    }

    void Start()
    {
        StartCoroutine(OnStartCoroutine());
    }

    protected virtual IEnumerator OnStartCoroutine()
    {
        yield return null;
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

    protected virtual void OnInitialized()
    {
    }

    protected virtual void InitObjects()
    {
    }

    void OnDestroy()
    {
        RemoveEventHandlers();
    }
}
