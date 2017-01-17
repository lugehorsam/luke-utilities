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

    public sealed override bool keepWaiting {
        get {
            if (RequestIsDone()) {

                TDatum[] serializedData = JsonUtility.FromJson<JsonArray<TDatum>> (GetRequestContent()).Data;
                data = serializedData;

            }
            return !RequestIsDone();
        }
    }

    protected abstract bool RequestIsDone();
    protected abstract string GetRequestContent();

}
