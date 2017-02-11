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

        public override bool RequestIsDone()
        {
            return request.isDone;
        }

        public override string GetRawContent()
        {
            Object asset = request.asset;
            TextAsset textAsset = asset as TextAsset;
            return textAsset.text;
        }
    }
}
