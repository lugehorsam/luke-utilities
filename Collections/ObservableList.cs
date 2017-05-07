using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections.ObjectModel;

public class ObservableList<T> : List<T>
{
    public event Action<T, int> OnRemove = delegate { };
    public event Action<T> OnAdd = delegate { };
    
    public new void Add(T item)
    {
        if (item == null) {
            return;
        }
        base.Add(item);
        OnAdd(item);
    }
    public new void Remove(T item)
    {
        if (item == null) {
            return;
        }
        int index = IndexOf (item);
        base.Remove(item);
        OnRemove(item, index);
    }
    public new void AddRange(IEnumerable<T> collection)
    {
        if (!collection.Any()) {
            return;
        }
        base.AddRange(collection);
        foreach (T element in collection) {
            OnAdd (element);
        }
    }
    public new void RemoveRange(int index, int count)
    {
        List<T> itemsToRemove = base.GetRange (index, count).ToList();
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
        List<T> previousItems = this.ToList();
        if (previousItems.Count <= 0) {
            return;
        }
        base.Clear();
        for (int i = 0; i < previousItems.Count; i++) {
            OnRemove (previousItems[i], i);
        }

    }
    public new void Insert(int index, T item)
    {
        if (item == null) {
            return;
        }
        base.Insert(index, item);
        OnAdd(item);
    }
    public new void InsertRange(int index, IEnumerable<T> collection)
    {
        if (!collection.Any()) {
            return;
        }
        base.InsertRange(index, collection);
        foreach (T item in collection) {
            OnAdd (item);
        }
    }
    public new void RemoveAll(Predicate<T> match)
    {
        List<T> itemsToRemove = base.FindAll (match).ToList();
        if (!itemsToRemove.Any()) {
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

    public ObservableList(IList<T> list)
    {
        foreach (T item in list) {
            Add (item);
        }
    }

    public ObservableList() {

    }

    public new T this[int index]
    {
        get
        {
            return base[index];
        }
        set
        {
            T oldValue = this[index];
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
}

