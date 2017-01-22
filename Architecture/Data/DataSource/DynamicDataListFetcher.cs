using System;
using System.Linq;
using UnityEngine;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections;

public class DynamicDataFetcher<TDatum, TDataRequest> : DataFetcher<TDatum, TDataRequest>
    where TDatum : struct
    where TDataRequest : DataRequest<TDatum>, new() {

    [SerializeField]
    private string cacheKey;

    [SerializeField]
    string baseUrl;

    public void WriteToCache ()
    {
        string stringToCache = JsonUtility.ToJson (new JsonArray<TDatum> (Data.ToArray ()));

        PlayerPrefs.SetString (cacheKey, stringToCache);
    }

    public bool TryReadFromCache ()
    {
        if (cacheKey == null) {
            return false;
        }

        if (PlayerPrefs.HasKey (cacheKey)) {
            data.AddRange(JsonUtility.FromJson<JsonArray<TDatum>> (PlayerPrefs.GetString (cacheKey)).Data);

            return true;
        }
        return false;
    }
}
