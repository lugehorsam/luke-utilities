using UnityEngine;
using System.Collections;

public abstract class DataRequest<TData> : CustomYieldInstruction where TData : struct {

    public TData[] Data {
        get {
            return data;
        }
    }

    protected TData[] data {
        private get;
        set;
    }
}
