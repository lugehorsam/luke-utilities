using System.Collections;
using UnityEngine;

public abstract class DataSource<TDatum, TDataRequest>
    : ScriptableObject
    where TDatum : struct
    where TDataRequest : DataRequest<TDatum>, new()
{

    public ObservableList<TDatum> Data
    {
        get { return data; }

    }
    protected readonly ObservableList<TDatum> data = new ObservableList<TDatum>();
    private readonly TDataRequest request;

    public IEnumerator FetchData()
    {
        TDataRequest dataRequest = new TDataRequest();
        yield return dataRequest;
        data.AddRange(dataRequest.Data);
    }
}
