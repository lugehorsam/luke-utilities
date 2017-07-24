using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Datum
{
    public class ResourcesRequest<TDatum> : DatumRequest<TDatum>
    {
        private readonly ResourceRequest request;
        private readonly string pathFromResources;

        public ResourcesRequest(string pathFromResources)
        {
            request = Resources.LoadAsync<TextAsset>(pathFromResources);
            this.pathFromResources = pathFromResources;
        }

        public override bool RequestIsDone()
        {
            return request.isDone;
        }

        public override string GetRawContent()
        {
            Object asset = request.asset;
            TextAsset textAsset = asset as TextAsset;
            try
            {
                return textAsset.text;
            }
            catch (NullReferenceException)
            {
               Diagnostics.Report("Could not found text asset at path " + pathFromResources);
               return "";
            }
        }
    }
}
