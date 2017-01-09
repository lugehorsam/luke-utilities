using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.ObjectModel;

public abstract class DataManager<TDatum, TBehavior> : GameBehavior
    where TDatum : struct
    where TBehavior : DatumBehavior<TDatum> {

    public ReadOnlyCollection<TDatum> Data {
        get {
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

    public event Action<TBehavior> OnBehaviorInitialized = (behavior) => { };

    [SerializeField]
    protected Prefab dataBehaviorPrefab;

    [SerializeField]
    int initialGameObjects = 10;

    ObjectPool<TBehavior> behaviorPool;

    Action unsubscribeFromSource;
    Action pushToSource;

    protected override void OnAwake ()
    {
        Init ();
    }

    void Init ()
    {

        InitBehaviorPool ();
        data.OnAdd += HandleNewDatum;
        data.OnRemove += HandleRemovedDatum;
        AddLocalData ();
    }

    public void Subscribe<TSourceData> (DataSource<TSourceData> newDataSource,
                                    int sourceDatumIndex) where TSourceData : struct, IComposite<TDatum>
    {

        if (data.Count > 0) {
            data.Clear ();
            unsubscribeFromSource ();
        }

        Action<TSourceData []> handleDataPublished = (TSourceData [] sourceData) => {
            if (sourceDatumIndex >= sourceData.Length) {
                return;
            }
            OnDataPublished (sourceData [sourceDatumIndex].GetCompositeData ());
        };

        newDataSource.OnDataPublish += handleDataPublished;

        unsubscribeFromSource = () => {
            newDataSource.OnDataPublish -= handleDataPublished;
        };

        pushToSource = () => {
            TSourceData [] newData = newDataSource.Data.ToArray ();
            newData [sourceDatumIndex].SetCompositeData (data);
            newDataSource.Set (newData);
        };
    }

    public void Subscribe (DataSource<TDatum> newDataSource)
    {

        pushToSource = () => newDataSource.Set (data.ToArray());
        if (data.Count > 0) {
            data.Clear ();
            unsubscribeFromSource ();
        }

        unsubscribeFromSource = () => {
            newDataSource.OnDataPublish -= OnDataPublished;
        };

        pushToSource = () => {
            newDataSource.Set (data.ToArray ());
        };

        newDataSource.OnDataPublish += OnDataPublished;
        OnDataPublished(newDataSource.Data.ToArray());
    }

    public void Push ()
    {
        pushToSource ();
    }

    public void TransferTo (DataManager<TDatum, TBehavior> receivingManager,
                               int insertionIndex,
                               TBehavior behavior)
    {
        if (receivingManager == this) {
            Diagnostics.Report ("Data manager " + this + "Trying to transfer data to itself");
        }

        List<TDatum> silenTDatum = data;
        List<TDatum> receivingSilenTDatum = receivingManager.data;

        RemoveHandlers (behavior);
        receivingManager.AddHandlers (behavior);
        silenTDatum.Remove (behavior.Datum);
        receivingSilenTDatum.Insert (insertionIndex, behavior.Datum);
        behaviorPool.TransferTo (receivingManager.behaviorPool, behavior, insertionIndex);
        HandleRemovedBehavior (behavior);
        receivingManager.HandleNewBehavior (behavior);
        Push ();
        receivingManager.Push ();
        TryReportBadConfig ();
    }
     
    protected abstract void HandleNewBehavior (TBehavior behavior);
    protected abstract void HandleRemovedBehavior (TBehavior behavior);

    protected virtual void AddHandlers (TBehavior behavior) { }
    protected virtual void RemoveHandlers (TBehavior behavior) { }
    protected virtual void AddLocalData () { }

    void OnDataPublished (TDatum [] publishedData)
    {

        List<TDatum> silenTDatum = data;
        TDatum [] oldData = new TDatum [silenTDatum.Count ()];
        silenTDatum.CopyTo (oldData);
        silenTDatum.Clear ();
        silenTDatum.AddRange (publishedData);

        int i = 0;
        while (i < publishedData.Length || i < oldData.Length) {
            if (i >= publishedData.Length) {
                HandleRemovedDatum (oldData [i], i);
            } else if (i >= oldData.Length) {
                HandleNewDatum (publishedData [i]);
            }
            i++;
        }
    }

    void HandleNewDatum (TDatum newDatum)
    {
        TBehavior behavior = behaviorPool.Release ();
        behavior.Datum = newDatum;

        AddHandlers (behavior);
        HandleNewBehavior (behavior);
    }

    void TryReportBadConfig ()
    {
        if (data.Count != Behaviors.Count) {
            Diagnostics.Report ("Data and behaviors have uneven counts " + data.ToFormattedString () + " , " + Behaviors.ToFormattedString ());
        }
    }

    void InitBehaviorPool ()
    {
        behaviorPool = new ObjectPool<TBehavior> (() => CreateAndInitBehavior (), initialGameObjects);
        behaviorPool.OnPool += HandleBehaviorPooled;
        behaviorPool.OnRelease += HandleBehaviorReleased;
    }

    void HandleBehaviorPooled (TBehavior behavior)
    {
        behavior.gameObject.SetActive (false);
    }

    void HandleBehaviorReleased (TBehavior behavior)
    {
        behavior.gameObject.SetActive (true);
    }

    void HandleRemovedDatum (TDatum oldDatum, int oldDatumIndex)
    {

        TBehavior behaviorToPool = Behaviors.First ((behavior) => behavior.Datum.Equals (oldDatum));
        RemoveHandlers (behaviorToPool);
        behaviorPool.Pool (behaviorToPool);
        HandleRemovedBehavior (behaviorToPool);
    }

    void HandleChangedDatum (TDatum changedDatum)
    {
        Behaviors.First ((behavior) => behavior.Datum.Equals (changedDatum)).Datum = changedDatum;
    }

    TBehavior CreateAndInitBehavior ()
    {
        TBehavior behavior = dataBehaviorPrefab.Instantiate<TBehavior> ();
        behavior.gameObject.SetActive (false);
        OnBehaviorInitialized (behavior);
        return behavior;
    }

    public override string ToString ()
    {
        return string.Format ("[DataManager: Name={0} Data={1}]", gameObject.name, Data.ToFormattedString());
    }
}
