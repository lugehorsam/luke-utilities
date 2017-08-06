using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/**

namespace Datum
{
    public abstract class WebDatumRequest<TDatum> : DatumRequest<TDatum> where TDatum : struct {

        [SerializeField] protected NetworkConfig networkConfig;

        protected abstract string Url
        {
            get;
        }

        protected abstract Dictionary<string, string> PostData { get; }

        UnityWebRequest request;

        public WebDatumRequest ()
        {
            request = UnityWebRequest.Post(Url, PostData);
            request.downloadHandler = new DownloadHandlerBuffer ();
            request.Send ();
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

        public override bool RequestIsDone()
        {
            return request.isDone;
        }

        public override string GetRawContent()
        {
            return request.downloadHandler.text;
        }
    }

}
**/
