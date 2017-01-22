using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSource<TDatum, TDataRequest> : DataFetcher<TDatum, TDataRequest>
    where TDataRequest : DataRequest<TDatum>, new()
{
    public void RegisterSubscriber(IDataSubscriber<TDatum> behavior)
    {
        Data.RegisterObserver(behavior.Data);
    }
}
