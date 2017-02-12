using System;
using Scripting;
using UnityEngine;

[Serializable]
public class TextDatum : ScriptObject, ISerializationCallbackReceiver
{
    public string Text
    {
        get { return args == null ? text : string.Format(text, args); }
    }

    [SerializeField] private string text;
    [SerializeField] private TextQuery[] args;

    public void OnAfterDeserialize()
    {

    }

    public void OnBeforeSerialize()
    {

    }
}
