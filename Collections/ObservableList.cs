using System.Collections.Generic;
using System;
using System.Linq;
using System.ComponentModel;

/// <summary>
/// Observable list.
/// </summary>
public class ObservableList<TDatum> : List<TDatum>
{   
    public event Action<TDatum, int> OnRemove = delegate { };
    public event Action<TDatum> OnAdd = delegate {};

    public new void Add(TDatum item)
    {
        if (item == null) {
            return;
        }
        base.Add(item);
        OnAdd(item);
    }
    public new void Remove(TDatum item)
    {
        if (item == null) {
            return;
        }
        int index = IndexOf (item);
        base.Remove(item);
        OnRemove(item, index);
    }
    public new void AddRange(IEnumerable<TDatum> collection)
    {
        if (collection.Count() <= 0) {
            return;
        }
        base.AddRange(collection);
        foreach (TDatum element in collection) {
            OnAdd (element);
        }
    }
    public new void RemoveRange(int index, int count)
    {
        List<TDatum> itemsToRemove = base.GetRange (index, count).ToList();
        if (itemsToRemove.Count <= 0) {
            return;
        }
        base.RemoveRange(index, count);
        for (int i = index; i < count; i++) {
            OnRemove (itemsToRemove[i], i);
        }
    }
    public new void Clear()
    {
        List<TDatum> previousItems = this.ToList();
        if (previousItems.Count <= 0) {
            return;
        }
        base.Clear();
        for (int i = 0; i < previousItems.Count; i++) {
            OnRemove (previousItems[i], i);
        }

    }
    public new void Insert(int index, TDatum item)
    {
        if (item == null) {
            return;
        }
        base.Insert(index, item);
        OnAdd(item);
    }
    public new void InsertRange(int index, IEnumerable<TDatum> collection)
    {
        if (collection.Count() <= 0) {
            return;
        }
        base.InsertRange(index, collection);
        foreach (TDatum item in collection) {
            OnAdd (item);
        }
    }
    public new void RemoveAll(Predicate<TDatum> match)
    {
        List<TDatum> itemsToRemove = base.FindAll (match).ToList();
        if (itemsToRemove.Count () <= 0) {
            return;
        }
        base.RemoveAll(match);
        for (int i = 0; i < itemsToRemove.Count; i++) {
            OnRemove (itemsToRemove [i], i);
        }
    }

    public void RemoveAll ()
    {
        RemoveAll ((data) => true);
    }

    public ObservableList(IList<TDatum> list)
    {
        foreach (TDatum item in list) {
            Add (item);
        }
    }

    public ObservableList() {

    }

    public new TDatum this[int index]
    {
        get
        {
            return base[index];
        }
        set
        {
            TDatum oldValue = this[index];
            base[index] = value;
            if (oldValue != null) {
                OnRemove (oldValue, index);
            }
            if (value != null) {
                OnAdd (value);
            }
        }
    }

    public override string ToString ()
    {
        return this.ToArray().ToString();
    }

    public void Bind(ObservableList<TDatum> thisList, ObservableList<TDatum> otherList)
    {
        List<TDatum> thisListSilent = thisList;
        List<TDatum> otherListSilent = otherList;
    }

    /**

    public void Bind<TSourceData> (DynamicDataSource<TSourceData> newDynamicDataSource,
                                    int sourceDatumIndex) where TSourceData : struct, IComposite<TDatum> {
        if (data.Count > 0) {
            data.Clear ();
            unsubscribeFromSource ();
        }

        Action<TSourceData []> handleDataPublished = (TSourceData [] sourceData) => {
            if (sourceDatumIndex >= sourceData.Length) {
                return;
            }
            HandleNewData (sourceData [sourceDatumIndex].GetCompositeData ());
        };

        newDynamicDataSource.Data += handleDataPublished;

        unsubscribeFromSource = () => {
            newDynamicDataSource.DataStream -= handleDataPublished;
        };

        pushToSource = () => {
            TSourceData [] newData = newDynamicDataSource.Data.ToArray ();
            newData [sourceDatumIndex].SetCompositeData (data);
            newDynamicDataSource.Set (newData);
        };
    }

**/
}
