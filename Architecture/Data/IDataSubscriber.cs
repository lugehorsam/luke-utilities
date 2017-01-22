using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataSubscriber<TDatum>
{
    ObservableList<TDatum> Data { get; }
}
