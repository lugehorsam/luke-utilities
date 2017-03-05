using UnityEngine;

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
                    Diagnostics.Log("{0} Data deserialzied to {1}", ToString(), _datum);
                    HandleAfterDeserialize();
                }
                return !requestIsDone;
            }
        }

        protected virtual TDatum DeserializeJson(string json)
        {
            return JsonUtility.FromJson<TDatum>(json);
        }

        protected virtual void HandleAfterDeserialize()
        {

        }

        public abstract bool RequestIsDone();
        public abstract string GetRawContent();
    }
}
