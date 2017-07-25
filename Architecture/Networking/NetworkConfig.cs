using UnityEngine;
using System.Linq;

public class NetworkConfig : ScriptableObject {

    public string UrlBase => urlBase;

    [SerializeField]
    string urlBase;

    public string BuildEndpoint (string [] restOfUrl)
    {


        string endpoint = urlBase;
        foreach (string domain in restOfUrl) {
            endpoint += string.Format("/{0}", domain);
        }

        return endpoint;
    }
}
