﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Scripting
{
    [Serializable]
    public class ScriptTable<TDatum> : JsonArray<TDatum>, ISerializationCallbackReceiver
    {        
        public string Id
        {
            get { return id; }
        }
        
        [SerializeField] private string id;

        public Variable[] Globals
        {
            get { return globals; }
        }

        [SerializeField] private Variable[] globals;

        public void OnBeforeSerialize() 
        {
            
        }

        public void OnAfterDeserialize() 
        {
            Diagnostics.Log("After deserialize globals are " + globals.ToFormattedString());
        }
    }
}