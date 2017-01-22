using System;

public abstract class DatumSource<TDatum, TRequest> : DataFetcher<TDatum, TRequest>
    where TRequest : DataRequest<TDatum>, new() {

    protected abstract TDatum CurrentDatum { get; }

    protected Action<TDatum> onDatumChanged = (datum) => { };

    public void RegisterSubscriber(IDatumBehavior<TDatum> behavior)
    {
        onDatumChanged += (newDatum) =>
        {
            behavior.Datum = CurrentDatum;
        };
    }
}
