using UnityEngine;
using UnityEngine.Networking;
using System;

public class WebDataRequest<TData> : DataRequest<TData> where TData : struct {

    public override bool keepWaiting {
        get {

            if (request.isError) {
                Diagnostics.Report ("Request error " + request.error);
            }

            if (request.isDone && (request.downloadHandler.text == null || request.downloadHandler.text == "")) {
                throw new NullReferenceException ("Received null data from network " + request.url + " , " + request.GetResponseHeaders());
            }

            if (request.isDone) {

                TData[] serializedData = JsonUtility.FromJson<JsonArray<TData>> (request.downloadHandler.text).Data;
                data = serializedData;

            }
            return !request.isDone;
        }
    }

    UnityWebRequest request;

    public WebDataRequest (string url, WWWForm postData) {
        request = UnityWebRequest.Post (url, postData);
        request.downloadHandler = new DownloadHandlerBuffer ();
        request.Send ();
    }
}
