﻿using System;
using UnityEngine;

public struct DiagnosticsData {
    public string DisplayName {
        get {
            return displayName;
        }
    }

    string displayName;

    public KeyCode? Key {
        get {
            return key;
        }
    }

    KeyCode? key;

    public Action Action {
        get {
            return action;
        }
    }

    Action action;

    public DiagnosticsData (Action action, string displayName, KeyCode? key = null)
    {
        this.displayName = displayName;
        this.action = action;
        this.key = key;
    }
}