using System;

namespace Utilities.Cache
{
    using UnityEngine;

    public class Entry<T> where T : class
    {
        private readonly string _key;
        private T _value;
        
        public T Value => _value ?? (_value = LookupValue());
        
        public Entry(string key)
        {
            _key = key;
        }

        public void SetIfUnassigned(T data)
        {
            Diag.Crumb(this, $"Setting value for key {_key} to be {data}");
            _value = _value ?? data;
        }

        public void Save()
        {
            if (_value == null)
                return;

            var valueAsString = JsonUtility.ToJson(_value);

            if (string.IsNullOrEmpty(valueAsString))
            {
                Diag.Report(new InvalidOperationException($"Parsed value for {_key} as null or empty string."));
                return;
            }

            PlayerPrefs.SetString(_key, valueAsString);
            Diag.Crumb(this, $"Saved key {_key} to {valueAsString}");
        }

        private T LookupValue()
        {
            string keyContents = PlayerPrefs.GetString(_key);
            
            Diag.Crumb(this, $"Looking up key contents for {_key} as {keyContents}");

            return PlayerPrefs.HasKey(_key) ? JsonUtility.FromJson<T>(keyContents) : GetDefaultValue();
        }
        
        public static implicit operator T(Entry<T> thisEntry)
        {
            return thisEntry.Value;
        }

        protected virtual T GetDefaultValue()
        {
            return default(T);
        }
    }    
}
