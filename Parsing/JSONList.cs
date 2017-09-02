using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Serializable
{
    [Serializable]
    public class JSONList<T> : ISerializationCallbackReceiver {
    
        [SerializeField] private T[] array;

        public List<T> List { get; } = new List<T>();

        public JSONList(){}

        public JSONList(IEnumerable<T> enumerable)
        {
            List.AddRange(enumerable);
        }    

        public void OnBeforeSerialize()
        {
            array = List.ToArray();
        }

        public void OnAfterDeserialize()
        {
            if (array != null)
                List.AddRange(array);
        }
        
        public static implicit operator List<T>(JSONList<T> thisList)
        {
            return thisList.List;
        }
    }    
}
