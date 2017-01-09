using UnityEngine;
using System.Collections;

public abstract class DataRequest<TDatum> : CustomYieldInstruction where TDatum : struct {

    public TDatum[] Data {
        get {
            return data;
        }
    }

    protected TDatum[] data {
        private get;
        set;
    }
}
