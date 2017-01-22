using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DatumSource<TDatum, TRequest> : DataFetcher<TDatum, TRequest>
    where TDatum : struct
    where TRequest : DataRequest<TDatum>, new() {

    protected abstract TDatum CurrentDatum { get; }

    public void RegisterSubscriber(IDatumSubscriber<TDatum> subscriber)
    {
        
    }
}
