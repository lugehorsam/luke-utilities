using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

[System.Serializable]
public class JsonArray<T>
{
    public T [] Data {
        get {
            return data;
        }
    }
    [SerializeField]
    T[] data;

    public JsonArray (T [] data)
    {
        this.data = data;
    }
}
