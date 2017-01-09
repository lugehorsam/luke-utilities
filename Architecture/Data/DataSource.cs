using System;
using System.Linq;
using UnityEngine;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections;

public class DataSource<TDatum> : DataSource
    where TDatum : struct {

    public ReadOnlyCollection<TDatum> Data {
        get {
            return new ReadOnlyCollection<TDatum> (shouldOverrideData ? overrideData : data);
        }
    }

    List<TDatum> data = new List<TDatum>();

    string CacheKey {
        get {
            return cacheKey;
        }
    }

    [SerializeField]
    private string cacheKey;

    [SerializeField]
    string baseUrl;

    public event Action<TDatum []> OnDataPublish {
        add {
            onDataPublish += value;
            onDataPublish (data.ToArray());
        }
        remove {
            onDataPublish -= value;
        }
    }

    event Action<TDatum[]> onDataPublish = (data) => { };

    [SerializeField] protected NetworkConfig networkConfig;
    [SerializeField] private bool shouldOverrideData;
    [SerializeField] private List<TDatum> overrideData;

    protected IEnumerator FetchData (string endPoint, Dictionary<string, string> posTDatum)
    {

        DataRequest<TDatum> request = CreateDataRequest (BuildEndpoint(endPoint), posTDatum);
        yield return request;
        HandleDataFetched (request.Data);

        Set (request.Data);
    }

    WebDataRequest<TDatum> CreateDataRequest (string endpoint, Dictionary<string, string> posTDatum)
    {
        WWWForm form = new WWWForm ();
        if (posTDatum != null) {
            foreach (KeyValuePair<string, string> postDatum in posTDatum) {
                form.AddField (postDatum.Key, postDatum.Value);
            }
        }
        WebDataRequest<TDatum> request = new WebDataRequest<TDatum> (endpoint, form);
        return request;
    }

    protected virtual void HandleDataFetched (TDatum [] data) { }

    public void WriteToCache ()
    {
        if (cacheKey == null || !EnableCache) {
            return;
        }
        string stringToCache = JsonUtility.ToJson (new JsonArray<TDatum> (data.ToArray ()));

        PlayerPrefs.SetString (cacheKey, stringToCache);
    }

    public bool TryReadFromCache ()
    {
        if (cacheKey == null || !EnableCache) {
            return false;
        }

        if (PlayerPrefs.HasKey (cacheKey)) {
            Set(JsonUtility.FromJson<JsonArray<TDatum>> (PlayerPrefs.GetString (cacheKey)).Data);

            return true;
        }
        return false;
    }

    public void Set (TDatum[] data)
    {

        if (this.data.SequenceEqual (data)) {
            return;
        }
        this.data = data.ToList();

        onDataPublish (data);
    }

    public void Push (TDatum datum)
    {
        List<TDatum> listCopy = new List<TDatum> (data);
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
