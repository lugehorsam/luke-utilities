using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class ScriptableTextInfo : System.Object {

    [HideInInspector]
    public string serializedName = "Scriptable Text Info";

    public string Text {
        get {
            return text;
        }
    }

    [SerializeField]
    string text;

    public float Delay {
        get {
            return delay;
        }
    }

    [SerializeField]
    float delay;

}
