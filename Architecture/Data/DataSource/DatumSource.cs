using System;
using System.Diagnostics;

public abstract class DatumSource<TDatum, TRequest> : DataFetcher<TDatum, TRequest>
    where TRequest : DataRequest<TDatum>, new() {

    protected abstract TDatum CurrentDatum { get; }

    protected Action<TDatum> onDatumChanged = (datum) => { };

    public void RegisterSubscriber(DatumBehavior<TDatum> datumBehavior)
    {
        datumBehavior.Datum = CurrentDatum;
        return;
        onDatumChanged += (newDatum) =>
        {
            datumBehavior.Datum = CurrentDatum;
        };
    }
}
