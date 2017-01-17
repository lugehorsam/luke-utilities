using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class WebDataRequest<TDatum> : DataRequest<TDatum> where TDatum : struct {

    [SerializeField] protected NetworkConfig networkConfig;

    protected abstract string Url
    {
        get;
    }

    protected abstract Dictionary<string, string> PostData { get; }

    UnityWebRequest request;

    public WebDataRequest ()
    {
        this.request = UnityWebRequest.Post(Url, PostData);
        this.request.downloadHandler = new DownloadHandlerBuffer ();
        this.request.Send ();
        WWWForm form = new WWWForm ();
        if (PostData != null) {
            foreach (KeyValuePair<string, string> postDatum in PostData) {
                form.AddField (postDatum.Key, postDatum.Value);
            }
        }
    }

    string BuildEndpoint (string endPoint)
    {
        return string.Format ("{0}/{1}/{2}", networkConfig.UrlBase, Url, endPoint);
    }

    protected override bool RequestIsDone()
    {
        return request.isDone;
    }

    protected override string GetRequestContent()
    {
        return request.downloadHandler.text;
    }
}
