using UnityEngine;
using System.Linq;

public class NetworkConfig : ScriptableObject {

    public string UrlBase {
        get {
            return urlBase;
        }
    }

    [SerializeField]
    string urlBase;

    public string BuildEndpoint (string [] restOfUrl)
    {
        Diagnostics.Log ("build endpoint called with " + restOfUrl.ToFormattedString());

        string endpoint = urlBase;
        foreach (string domain in restOfUrl) {
            endpoint += string.Format("/{0}", domain);
        }
        Diagnostics.Log ("config " + this + " built url " + restOfUrl.ToFormattedString());
        return endpoint;
    }
}
