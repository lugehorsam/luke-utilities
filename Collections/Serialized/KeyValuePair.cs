using UnityEngine;

namespace Utilities.Serializable
{
    [System.Serializable]
    public class KeyValuePair<T, K> {

        public T Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public K Value
        {
            get { return _value; }
            set { _value = value; }
        }

        [SerializeField] T _key;
        [SerializeField] K _value;
    }  
}