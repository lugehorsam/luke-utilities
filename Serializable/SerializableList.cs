using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rune
{
    [Serializable]
    public class SerializableList<T> : List<T>, ISerializationCallbackReceiver
    {
        [SerializeField] private T[] serializedElements;
        
        public void OnBeforeSerialize()
        {
            serializedElements = ToArray();
        }

        public void OnAfterDeserialize()
        {
            AddRange(serializedElements);
        }
    }    
}

