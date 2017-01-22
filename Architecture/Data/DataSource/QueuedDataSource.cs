using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueuedDataSource<TDatum, TRequest> : DatumSource<TDatum, TRequest>
    where TDatum : struct, IEnumerator
    where TRequest : DataRequest<TDatum>, new() {

    readonly EnumeratorQueue<TDatum> dataQueue = new EnumeratorQueue<TDatum>();

    protected override TDatum CurrentDatum
    {
        get { return  dataQueue.Current; }
    }
}
