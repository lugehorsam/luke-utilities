using System.Collections.Generic;
using System;
using System.Linq;
using System.ComponentModel;

/// <summary>
/// Observable list.
/// </summary>
public class ObservableList<TData> : List<TData>
{   
    public event Action<TData, int> OnRemove = delegate { };
    public event Action<TData> OnAdd = delegate {};

    public new void Add(TData item)
    {
        if (item == null) {
            return;
        }
        base.Add(item);
        OnAdd(item);
    }
    public new void Remove(TData item)
    {
        if (item == null) {
            return;
        }
        int index = IndexOf (item);
        base.Remove(item);
        OnRemove(item, index);
    }
    public new void AddRange(IEnumerable<TData> collection)
    {
        if (collection.Count() <= 0) {
            return;
        }
        base.AddRange(collection);
        foreach (TData element in collection) {
            OnAdd (element);
        }
    }
    public new void RemoveRange(int index, int count)
    {
        List<TData> itemsToRemove = base.GetRange (index, count).ToList();
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
        List<TData> previousItems = this.ToList();
        if (previousItems.Count <= 0) {
            return;
        }
        base.Clear();
        for (int i = 0; i < previousItems.Count; i++) {
            OnRemove (previousItems[i], i);
        }

    }
    public new void Insert(int index, TData item)
    {
        if (item == null) {
            return;
        }
        base.Insert(index, item);
        OnAdd(item);
    }
    public new void InsertRange(int index, IEnumerable<TData> collection)
    {
        if (collection.Count() <= 0) {
            return;
        }
        base.InsertRange(index, collection);
        foreach (TData item in collection) {
            OnAdd (item);
        }
    }
    public new void RemoveAll(Predicate<TData> match)
    {
        List<TData> itemsToRemove = base.FindAll (match).ToList();
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

    public ObservableList(IList<TData> list)
    {
        foreach (TData item in list) {
            Add (item);
        }
    }

    public ObservableList() {

    }

    public new TData this[int index]
    {
        get
        {
            return base[index];
        }
        set
        {
            TData oldValue = this[index];
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

    public static void Observe<T> (ObservableList<T> observedList, T[] observerArray, Action<T[]> handleNewArray = null)
    {
        observedList.OnAdd += (newData) => {
            observerArray = observedList.ToArray ();
            Observe (observedList, observerArray);
            if (handleNewArray != null) {
                handleNewArray (observerArray);
            }
            Diagnostics.Log ("new array contents are " + observerArray.ToFormattedString ());
        };
        observedList.OnRemove += (removedData, removalIndices) => {
            observerArray = observedList.ToArray ();
            Observe (observedList, observerArray);
            if (handleNewArray != null) {
                handleNewArray (observerArray);
            }
        };
    }
}
