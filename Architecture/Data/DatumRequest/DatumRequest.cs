using UnityEngine;

namespace Datum
{
    public abstract class DatumRequest<TDatum> : CustomYieldInstruction
    {

        public ObservableList<TDatum> Data
        {
            get { return _data; }
        }

        private ObservableList<TDatum> _data;

        public sealed override bool keepWaiting
        {
            get
            {
                bool requestIsDone = RequestIsDone();
                if (requestIsDone)
                {
                    string requestContent = GetRequestContent();
                    _data = DeserializeJson(requestContent);
                }
                return !requestIsDone;
            }
        }

        protected virtual ObservableList<TDatum> DeserializeJson(string json)
        {
            return new ObservableList<TDatum>(JsonUtility.FromJson<JsonArray<TDatum>>(json).Data);
        }

        protected abstract bool RequestIsDone();
        protected abstract string GetRequestContent();
    }
}
