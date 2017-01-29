using UnityEngine;

namespace Datum
{
    public abstract class ResourcesRequest<TDatum> : DatumRequest<TDatum>
    {
        protected abstract string PathFromResources { get; }

        private readonly ResourceRequest request;

        public ResourcesRequest()
        {
            request = Resources.LoadAsync<TextAsset>(PathFromResources);
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
