using UnityEngine;
using System;
using System.Collections;

public abstract class LerpBinding<TProperty, TComponent> : PropertyBinding<TProperty, TComponent>
    where TProperty : struct
    where TComponent : Component 
{
    public bool LerpOnEnqueue {
        get {
            return lerpOnEnqueue;
        }
        set {
            lerpOnEnqueue = value;
        }            
    }

    bool lerpOnEnqueue = true;

    public event Action<LerpBinding<TProperty, TComponent>> OnLerp = (binding) => { };

    public EnumeratorQueue LerpQueue
    {
        get { return lerpQueue; }
    }

    readonly EnumeratorQueue lerpQueue = new EnumeratorQueue();

    private readonly MonoBehaviour coroutineRunner;

    protected LerpBinding(MonoBehaviour coroutineRunner, GameObject gameObject) : base(gameObject)
    {
        this.coroutineRunner = coroutineRunner;
    }

    public abstract Func<TProperty, TProperty, TProperty> GetRandomizeDelegate ();
    public abstract TProperty AddProperty (TProperty property1, TProperty property2);

    public void IncrementProperty (TProperty incrementValue)
    {
        SetProperty (AddProperty (GetProperty (), incrementValue));
    }

    public void AddPropertyRandomly (TProperty min, TProperty max)
    {
        TProperty randValue = GetRandomizeDelegate () (min, max);
        TProperty newValue = AddProperty (GetProperty (), randValue);
        SetProperty (newValue);
    }

    public void SetPropertyRandomly (TProperty min, TProperty max)
    {
        TProperty randValue = GetRandomizeDelegate () (min, max);
        SetProperty (randValue);
    }

    protected abstract Func<TProperty, TProperty, float, TProperty> GetLerpDelegate ();

    public void EnqueueFiniteLerp (FiniteLerp<TProperty> lerp, bool stopOtherEnumerators = true)
    {
        lerpQueue.Add(ApplyFiniteLerp(lerp));

        if (stopOtherEnumerators) 
        {
            StopOtherEnumerators();
        }
 
        ActivateQueueIfNeeded ();
    }

    public void EnqueueInfiniteLerp(InfiniteLerp<TProperty> lerp, bool stopOtherEnumerators = true)
    {
        lerpQueue.Add(ApplyInfiniteLerp(lerp));
        if (stopOtherEnumerators)
        {
            StopOtherEnumerators();
        }

        ActivateQueueIfNeeded();
    }


    void StopOtherEnumerators()
    {
        while (lerpQueue.Count > 1)
        {
            lerpQueue.StopCurrentEnumerator();
        }
    }

    void ActivateQueueIfNeeded ()
    {
        if (lerpQueue.Current != null && lerpOnEnqueue) {
            coroutineRunner.StartCoroutine (lerpQueue);
        }
    }

    IEnumerator ApplyFiniteLerp (FiniteLerp<TProperty> lerp)
    {
        while (!lerp.HasReachedTargetTime) {
            TProperty lerpedProperty = lerp.GetLerpedProperty (GetProperty (), GetLerpDelegate());
            SetProperty (lerpedProperty);
            lerp.UpdateTime ();
            OnLerp (this);
            yield return null;
        }
    }

    IEnumerator ApplyInfiniteLerp(InfiniteLerp<TProperty> lerp)
    {
        while (true)
        {
            IncrementProperty(lerp.UnitsPerSecond);
            OnLerp(this);
            yield return null;
        }
    }

    public void StopAllLerps()
    {
        lerpQueue.StopCurrentEnumerator();
    }
}
