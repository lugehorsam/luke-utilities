using System;
using System.Collections;
using UnityEngine;

public class DataFetcher<TDatum, TDataRequest> : ScriptableObject
    where TDataRequest : DataRequest<TDatum>, new()
{
    protected ObservableList<TDatum> Data
    {
        get { return data; }
    }

    ObservableList<TDatum> data = new ObservableList<TDatum>();

    public IEnumerator FetchData()
    {
        yield return null;
        TDataRequest dataRequest = new TDataRequest();
        yield return dataRequest;

        if (dataRequest.Data == null)
            throw new NullReferenceException("Data request has no data.");

        data.AddRange(dataRequest.Data);
        Diagnostics.Log("added data " + data.ToFormattedString());
        OnAfterFetchedData();
    }

    protected virtual void OnAfterFetchedData()
    {
    }
}
