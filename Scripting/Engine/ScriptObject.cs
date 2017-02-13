using UnityEngine;

namespace Scripting
{
    public class ScriptObject : CollectedClass<ScriptObject> {

        public string Id
        {
            get { return id; }
        }

        [SerializeField] private string id;

        public Condition ShouldAppear
        {
            get { return shouldAppear; }
        }

        [SerializeField] private Condition shouldAppear;

        public string Display
        {
            get { return display ?? id.ToUpper(); }
        }

        [SerializeField] private string display;
    }
}
