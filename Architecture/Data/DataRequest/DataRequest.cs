using UnityEngine;

public abstract class DataRequest<TTDatum> : CustomYieldInstruction {

    public TTDatum[] Data {
        get {
            return data;
        }
    }

    private TTDatum[] data;

    public sealed override bool keepWaiting {
        get
        {
            bool requestIsDone = RequestIsDone();
            if (requestIsDone)
            {
                string requestContent = GetRequestContent();
                data = DeserializeJson(requestContent);
            }
            return !requestIsDone;
        }
    }

    protected virtual TTDatum[] DeserializeJson(string json)
    {
        Debug.Log("Deserializing json called");
        return JsonUtility.FromJson<JsonArray<TTDatum>> (json).Data;
    }

    protected abstract bool RequestIsDone();
    protected abstract string GetRequestContent();
}
