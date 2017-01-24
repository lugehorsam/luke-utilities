﻿using System.Collections;

public class QueuedDataSource<TDatum, TRequest> : DatumSource<TDatum, TRequest>
    where TDatum : IEnumerator
    where TRequest : DataRequest<TDatum>, new() {

    readonly EnumeratorQueue<TDatum> dataQueue = new EnumeratorQueue<TDatum>();

    protected override TDatum CurrentDatum
    {
        get
        {
            return dataQueue.Current;
        }
    }

    protected override void OnAfterFetchedData()
    {
        while (true)
        {
            if (dataQueue.MoveNext())
            {
                onDatumChanged(dataQueue.Current);
            }
        }
    }
}