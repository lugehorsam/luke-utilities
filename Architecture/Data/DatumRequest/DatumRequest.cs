using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Datum
{
    public abstract class DatumRequest<TDatum> : CustomYieldInstruction
    {

        public TDatum Datum
        {
            get { return _datum; }
        }

        private TDatum _datum;

        public sealed override bool keepWaiting
        {
            get
            {
                bool requestIsDone = RequestIsDone();
                if (requestIsDone)
                {
                    string requestContent = GetRawContent();
                    Diagnostics.Log("Request {0} got raw content {1}", ToString(), requestContent);
                    _datum = DeserializeJson(requestContent);
                    HandleAfterDeserialize(requestContent);
                }
                return !requestIsDone;
            }
        }

        protected virtual TDatum DeserializeJson(string json)
        {
            return JsonUtility.FromJson<TDatum>(json);
        }

        protected virtual void HandleAfterDeserialize(string rawContent)
        {

        }

        public abstract bool RequestIsDone();
        public abstract string GetRawContent();
    }
}
