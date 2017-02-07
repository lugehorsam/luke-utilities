using UnityEngine;

namespace Datum
{
    public class ResourcesRequest<TDatum> : DatumRequest<TDatum>
    {
        private readonly ResourceRequest request;

        public ResourcesRequest(string pathFromResources)
        {
            request = Resources.LoadAsync<TextAsset>(pathFromResources);
        }

        protected override bool RequestIsDone()
        {
            return request.isDone;
        }

        protected override string GetRequestContent()
        {
            Object asset = request.asset;
            TextAsset textAsset = asset as TextAsset;
            return textAsset.text;
        }
    }
}
