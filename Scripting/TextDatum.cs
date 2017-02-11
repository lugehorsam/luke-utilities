using System;
using Scripting;
using UnityEngine;

[Serializable]
public class TextDatum : ScriptObject
{
    public string Text
    {
        get { return insert == null ? text : string.Format(text, insert); }
    }

    [SerializeField] private string text;
    [SerializeField] private TextInsert[] insert;
}
