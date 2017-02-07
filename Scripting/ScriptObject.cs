using UnityEngine;

namespace Scripting
{
    public class ScriptObject<T> : CollectedClass<ScriptObject<T>> {

        public string Id
        {
            get { return id; }
        }

        [SerializeField] private string id;

        public Conditional ShouldAppear
        {
            get { return shouldAppear; }
        }

        [SerializeField] private Conditional shouldAppear;

        public string DisplayName
        {
            get { return display ?? id.ToUpper(); }
        }

        [SerializeField] private string display;
    }
}
