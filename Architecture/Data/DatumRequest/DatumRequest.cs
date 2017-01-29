using UnityEngine;

namespace Datum
{
    public abstract class DatumRequest<TDatum> : CustomYieldInstruction
    {

        public TDatum Datum
        {
            get { return datum; }
        }

        private TDatum datum;

        public sealed override bool keepWaiting
        {
            get
            {
                bool requestIsDone = RequestIsDone();
                if (requestIsDone)
                {
                    string requestContent = GetRequestContent();
                    datum = DeserializeJson(requestContent);
                }
                Debug.Log(this.GetType().ToString() + "Request is done " + requestIsDone);
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
