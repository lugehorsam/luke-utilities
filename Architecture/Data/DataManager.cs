using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.ObjectModel;

public abstract class DataManager<TData, TBehavior> : MonoBehaviour
    where TData : struct
    where TBehavior : DatumBehavior<TData> {

    public ReadOnlyCollection<TData> Data {
        get {
            return new ReadOnlyCollection<TData> (data);
        }
    }

    protected readonly ObservableList<TData> data = new ObservableList<TData>();

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

    void Awake ()
    {
        Init ();
    }

    void Init ()
    {
        Diagnostics.Log ("Init called");
        InitBehaviorPool ();
        data.OnAdd += HandleNewDatum;
        data.OnRemove += HandleRemovedDatum;
        AddLocalData ();
    }

    public void Subscribe<TSourceData> (DataSource<TSourceData> newDataSource,
                                    int sourceDatumIndex) where TSourceData : struct, IComposite<TData>
    {
        Diagnostics.Log ("Subscription: " + this + " to " + newDataSource, LogType.DataFlow);
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

    public void Subscribe (DataSource<TData> newDataSource)
    {
        Diagnostics.Log ("Subscription: " + this + " to " + newDataSource, LogType.DataFlow);
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
    }

    public void Push ()
    {
        pushToSource ();
    }

    public void TransferTo (DataManager<TData, TBehavior> receivingManager,
                               int insertionIndex,
                               TBehavior behavior)
    {
        if (receivingManager == this) {
            Diagnostics.Report ("Data manager " + this + "Trying to transfer data to itself");
        }
        Diagnostics.Log ("Transfer from " + this + " to" + receivingManager + "started", LogType.DataFlow);
        List<TData> silentData = data;
        List<TData> receivingSilentData = receivingManager.data;

        RemoveHandlers (behavior);
        receivingManager.AddHandlers (behavior);
        silentData.Remove (behavior.Datum);
        receivingSilentData.Insert (insertionIndex, behavior.Datum);
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
    protected virtual void InitComponents () { }
    protected virtual void AddLocalData () { }

    void OnDataPublished (TData [] publishedData)
    {
        Diagnostics.Log ("[Heard published data] " + this, LogType.DataFlow);
        List<TData> silentData = data;
        TData [] oldData = new TData [silentData.Count ()];
        silentData.CopyTo (oldData);
        silentData.Clear ();
        silentData.AddRange (publishedData);

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

    void HandleNewDatum (TData newDatum)
    {
        TBehavior behavior = behaviorPool.Release ();
        behavior.Datum = newDatum;
        Diagnostics.Log (this.name + ": released behavior with datum " + behavior.Datum, LogType.Behaviors);
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

    void HandleRemovedDatum (TData oldDatum, int oldDatumIndex)
    {
        Diagnostics.Log ("Handle removed datum " + oldDatum + " from " + this.name, LogType.DataFlow);
        TBehavior behaviorToPool = Behaviors.First ((behavior) => behavior.Datum.Equals (oldDatum));
        RemoveHandlers (behaviorToPool);
        behaviorPool.Pool (behaviorToPool);
        HandleRemovedBehavior (behaviorToPool);
    }

    void HandleChangedDatum (TData changedDatum)
    {
        Behaviors.First ((behavior) => behavior.Datum.Equals (changedDatum)).Datum = changedDatum;
    }

    TBehavior CreateAndInitBehavior ()
    {
        TBehavior behavior = dataBehaviorPrefab.Instantiate<TBehavior> ();
        behavior.Init ();
        behavior.gameObject.SetActive (false);
        OnBehaviorInitialized (behavior);
        return behavior;
    }

    public override string ToString ()
    {
        return string.Format ("[DataManager: Name={0} Data={1}]", gameObject.name, Data.ToFormattedString());
    }
}
