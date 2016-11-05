using System;
using System.Linq;
using UnityEngine;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections;

public abstract class DataSource<TData> : DataSource
    where TData : struct {

    public ReadOnlyCollection<TData> Data {
        get {
            return new ReadOnlyCollection<TData> (data);
        }
    }

    List<TData> data = new List<TData>();

    string cacheKey {
        get {
            return baseUrl;
        }
    }

    [SerializeField]
    string baseUrl;

    public event Action<TData []> OnDataPublish {
        add {
            onDataPublish += value;
            onDataPublish (data.ToArray());
        }
        remove {
            onDataPublish -= value;
        }
    }

    event Action<TData[]> onDataPublish = (data) => { };

    [SerializeField]
    protected NetworkConfig networkConfig;

    protected IEnumerator FetchData (string endPoint, Dictionary<string, string> postData)
    {
        Diagnostics.Log ("network config is on " + this + " is " + networkConfig);
        DataRequest<TData> request = CreateDataRequest (BuildEndpoint(endPoint), postData);
        yield return request;
        HandleDataFetched (request.Data);
        Diagnostics.Log ("request data is " + request.Data.ToFormattedString ());
        Set (request.Data);
    }

    WebDataRequest<TData> CreateDataRequest (string endpoint, Dictionary<string, string> postData)
    {
        WWWForm form = new WWWForm ();
        if (postData != null) {
            foreach (KeyValuePair<string, string> postDatum in postData) {
                form.AddField (postDatum.Key, postDatum.Value);
            }
        }
        WebDataRequest<TData> request = new WebDataRequest<TData> (endpoint, form);
        return request;
    }

    protected virtual void HandleDataFetched (TData [] data) { }

    public void WriteToCache ()
    {
        if (cacheKey == null || !EnableCache) {
            return;
        }
        string stringToCache = JsonUtility.ToJson (new JsonArray<TData> (data.ToArray ()));
        Diagnostics.Log ("Source " + this + " Caching data " + stringToCache, LogType.DataFlow);
        PlayerPrefs.SetString (cacheKey, stringToCache);
    }

    public bool TryReadFromCache ()
    {
        if (cacheKey == null || !EnableCache) {
            return false;
        }

        if (PlayerPrefs.HasKey (cacheKey)) {
            Set(JsonUtility.FromJson<JsonArray<TData>> (PlayerPrefs.GetString (cacheKey)).Data);
            Diagnostics.Log ("Source " + this + " received data " + data.ToFormattedString () + " from cache " + " length is " + data.Count, LogType.DataFlow);
            return true;
        }
        return false;
    }

    public void Set (TData[] data)
    {
        Diagnostics.Log (this.ToString() + " is being pushed to", LogType.DataFlow);
        if (this.data.SequenceEqual (data)) {
            return;
        }
        this.data = data.ToList();
        Diagnostics.Log ("and is publishing data ", LogType.DataFlow);
        onDataPublish (data);
    }

    public void Push (TData datum)
    {
        List<TData> listCopy = new List<TData> (data);
        listCopy.Add (datum);
        Set (listCopy.ToArray());
    }

    public override string ToString ()
    {
        return string.Format ("[DataSource] {0} {1}", cacheKey, data.ToFormattedString ());
    }

    string BuildEndpoint (string endPoint)
    {
        return string.Format ("{0}/{1}/{2}", networkConfig.UrlBase, baseUrl, endPoint);
    }
}

public class DataSource : ScriptableObject
{
    public static bool EnableCache {
        get {
            return enableCache;
        }
        set {
            enableCache = value;
        }
    }

    static bool enableCache = true;
}
