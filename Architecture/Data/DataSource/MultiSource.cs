using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiSource<TDatum, TRequest> : DataFetcher<TDatum, TRequest>
where TDatum : struct
where TRequest : DataRequest<TDatum>, new() {

}
