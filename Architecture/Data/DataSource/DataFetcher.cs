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
        TDataRequest dataRequest = new TDataRequest();
        yield return dataRequest;
        data.AddRange(dataRequest.Data);
        OnAfterFetchedData();
    }

    protected virtual void OnAfterFetchedData()
    {
    }
}
