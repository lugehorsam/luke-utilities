﻿using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using System.Linq;

public abstract class DataSubscriber<TDatum, TBehavior> : GameBehavior, IDataSubscriber<TDatum>
    where TBehavior : DatumBehavior<TDatum> {

    public ObservableList<TDatum> Data {
        get { return data; }
    }

    readonly ObservableList<TDatum> data = new ObservableList<TDatum>();

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

    protected virtual void HandleNewBehavior(TBehavior behavior)
    {
    }

    protected virtual void HandleRemovedBehavior(TBehavior behavior)
    {
    }

    protected virtual void AddHandlers (TBehavior behavior) { }
    protected virtual void RemoveHandlers (TBehavior behavior) { }
    protected virtual ObservableList<TDatum> GetOverrideData ()
    {
        return null;
    }

    protected override void OnAwake()
    {
        InitBehaviorPool ();

        data.OnAdd += HandleNewDatum;
        data.OnRemove += HandleRemovedDatum;
    }

    void InitBehaviorPool ()
    {
        behaviorPool = new GameObjectPool<TBehavior> (dataBehaviorPrefab, initialGameObjects);
    }

    void HandleNewDatum (TDatum newDatum)
    {
        TBehavior behavior = behaviorPool.Release ();
        behavior.Datum = newDatum;
        Diagnostics.Log("releasing " + newDatum);
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
