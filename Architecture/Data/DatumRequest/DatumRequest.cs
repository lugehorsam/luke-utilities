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
                    string requestContent = GetRequestContent();
                    _datum = DeserializeJson(requestContent);
                }
                return !requestIsDone;
            }
        }

        protected virtual TDatum DeserializeJson(string json)
        {
            return JsonUtility.FromJson<TDatum>(json);
        }

        protected abstract bool RequestIsDone();
        protected abstract string GetRequestContent();
    }
}
