using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class ScriptableTextInfo : System.Object {

    [HideInInspector]
    public string serializedName = "Scriptable Text Info";

    public string Text => text;

    [SerializeField]
    string text;

    public float Delay => delay;

    [SerializeField]
    float delay;

}
