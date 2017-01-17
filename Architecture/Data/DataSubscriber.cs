using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using System.Linq;

public abstract class DataSubscriber<TDatum, TBehavior> : GameBehavior
    where TDatum : struct
    where TBehavior : DatumBehavior<TDatum> {

    public ReadOnlyCollection<TDatum> Data {
        get
        {
            return new ReadOnlyCollection<TDatum> (data);
        }
    }

    protected readonly ObservableList<TDatum> data = new ObservableList<TDatum>();

    public ReadOnlyCollection<TBehavior> Behaviors {
        get {
            return new ReadOnlyCollection<TBehavior>(behaviorPool.ReleasedObjects
                .OrderBy((behavior) => data.IndexOf(behavior.Datum))
                .ToList());
        }
    }

    [SerializeField]
    protected Prefab dataBehaviorPrefab;

    [SerializeField]
    int initialGameObjects = 10;

    protected ObjectPool<TBehavior> behaviorPool;

    public void Observe(ObservableList<TDatum> data)
    {
        data.Observe(data);
    }

    [SerializeField] private bool overrideData;

    void HandleChangedDatum (TDatum changedDatum)
    {
        Behaviors.First ((behavior) => behavior.Datum.Equals (changedDatum)).Datum = changedDatum;
    }

    protected abstract void HandleNewBehavior (TBehavior behavior);
    protected abstract void HandleRemovedBehavior (TBehavior behavior);

    protected virtual void AddHandlers (TBehavior behavior) { }
    protected virtual void RemoveHandlers (TBehavior behavior) { }
    protected virtual ObservableList<TDatum> GetOverrideData ()
    {
        return null;
    }

    protected override void OnAwake()
    {
        InitBehaviorPool ();
        if (overrideData)
        {
            Observe(GetOverrideData());
        }
    }

    void InitBehaviorPool ()
    {
        behaviorPool = new GameObjectPool<TBehavior> (dataBehaviorPrefab, initialGameObjects);
    }

    void HandleNewDatum (TDatum newDatum)
    {
        TBehavior behavior = behaviorPool.Release ();
        behavior.Datum = newDatum;
        AddHandlers (behavior);
        HandleNewBehavior (behavior);
    }

    void HandleRemovedDatum (TDatum oldDatum, int oldDatumIndex)
    {
        TBehavior behaviorToPool = Behaviors.First ((behavior) => behavior.Datum.Equals (oldDatum));
        RemoveHandlers (behaviorToPool);
        behaviorPool.Pool (behaviorToPool);
        HandleRemovedBehavior (behaviorToPool);
    }
}
