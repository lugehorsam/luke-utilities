using UnityEngine;
using System.Collections;

public abstract class DataRequest<TDatum> : CustomYieldInstruction {

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
            if (requestIsDone)
            {
                string requestContent = GetRequestContent();
                TDatum[] serializedData = DeserializeJson(requestContent);
                data = serializedData;
            }
            return !requestIsDone;
        }
    }
    protected virtual TDatum[] ProcessData(TDatum[] data)
    {
        return data;
    }

    protected virtual TDatum[] DeserializeJson(string json)
    {
        return JsonUtility.FromJson<JsonArray<TDatum>> (json).Data;
    }

    protected abstract bool RequestIsDone();
    protected abstract string GetRequestContent();

}
