using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public abstract class Theme : ScriptableObject {
    [SerializeField]
    string id;
    [SerializeField]
    string displayName;
}
