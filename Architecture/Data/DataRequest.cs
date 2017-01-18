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
        get
        {
            bool requestIsDone = RequestIsDone();
            Debug.Log("request is done " + requestIsDone);
            if (requestIsDone) {

                TDatum[] serializedData = JsonUtility.FromJson<JsonArray<TDatum>> (GetRequestContent()).Data;
                data = serializedData;
                Debug.Log("Dat ais " + data.ToFormattedString());
            }
            return !requestIsDone;
        }
    }

    protected abstract bool RequestIsDone();
    protected abstract string GetRequestContent();

}
