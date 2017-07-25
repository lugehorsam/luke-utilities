using System;
using UnityEngine;

public struct DiagnosticsData {
    public string DisplayName => displayName;

    string displayName;

    public KeyCode? Key => key;

    KeyCode? key;

    public Action Action => action;

    Action action;

    public DiagnosticsData (Action action, string displayName, KeyCode? key = null)
    {
        this.displayName = displayName;
        this.action = action;
        this.key = key;
    }
}
