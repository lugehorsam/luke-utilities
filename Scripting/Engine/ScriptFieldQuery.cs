using UnityEngine;
using System;

namespace Scripting
{
    [Serializable]    
    public class ScriptFieldQuery
    {
        public string Property
        {
            get
            {
                return property;
            }
        }
        
        [SerializeField]
        private string property;

        public string Value
        {
            get
            {
                return value;
            }
        }
        
        [SerializeField]
        private string value;
    }   
}
