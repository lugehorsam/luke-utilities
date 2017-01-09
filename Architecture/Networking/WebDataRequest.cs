using UnityEngine;
using UnityEngine.Networking;
using System;

public class WebDataRequest<TDatum> : DataRequest<TDatum> where TDatum : struct {

    public override bool keepWaiting {
        get {

            if (request.isError) {
                Diagnostics.Report ("Request error " + request.error);
            }

            if (request.isDone && (request.downloadHandler.text == null || request.downloadHandler.text == "")) {
                throw new NullReferenceException ("Received null data from network " + request.url + " , " + request.GetResponseHeaders());
            }

            if (request.isDone) {

                TDatum[] serializedData = JsonUtility.FromJson<JsonArray<TDatum>> (request.downloadHandler.text).Data;
                data = serializedData;

            }
            return !request.isDone;
        }
    }

    UnityWebRequest request;

    public WebDataRequest (string url, WWWForm posTDatum) {
        request = UnityWebRequest.Post (url, posTDatum);
        request.downloadHandler = new DownloadHandlerBuffer ();
        request.Send ();
    }
}
