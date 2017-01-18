using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions {

    public static void Observe<TDatum, TList> (this TList observerList, ObservableList<TDatum> observedList)
    where TList : List<TDatum> {

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
