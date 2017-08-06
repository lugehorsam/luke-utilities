using UnityEngine;

namespace Utilities.Datum
{
    public abstract class DatumRequest<TDatum> : CustomYieldInstruction
    {
        public TDatum Datum => _datum;

        private TDatum _datum;

        public sealed override bool keepWaiting
        {
            get
            {
                bool requestIsDone = RequestIsDone();
                if (requestIsDone)
                {
                    string requestContent = GetRawContent();
                    _datum = DeserializeJson(requestContent);
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
