using System.Collections;
using UnityEngine;

public class DataFetcher<TDatum, TDataRequest> : ScriptableObject
    where TDatum : struct
    where TDataRequest : DataRequest<TDatum>, new()
{
    protected TDatum[] Data
    {
        get { return dataRequest.Data; }
    }
    private TDataRequest dataRequest;

    public IEnumerator FetchData()
    {
         dataRequest = new TDataRequest();
         yield return dataRequest;
    }
}
