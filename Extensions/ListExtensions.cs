using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions {

    public static void Observe<T> (this List<T> observerList, ObservableList<T> observedList)
    {
        observerList.AddRange(observedList);

        observedList.OnAdd += (newData) =>
        {
            observerList.Add(newData);
        };

        observedList.OnRemove += (removedData, removalIndices) =>
        {
            observedList.Remove(removedData);
        };
    }
}
