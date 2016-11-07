using UnityEngine;
using System.Reflection;
using System;
using System.Collections.Generic;

public static class Diagnostics {

    public static LogType CurrentLogType {
        get {
            return currentLogType;
        }
        set {
            currentLogType = value;
        }
    }

    static LogType currentLogType = LogType.All; //override here

    public static void Report (string message)
    {
        Debug.LogWarning (message);
    }

    public static void Report (Exception e)
    {
        Debug.LogWarning (e.Message);
    }

    public static void Log (string log, LogType logType = LogType.All)
    {
        if (CurrentLogType != LogType.None && (CurrentLogType == LogType.All || CurrentLogType == logType)) {
            Debug.Log (string.Format("[{0}]:{1}", logType, log));
        }
    }

    public static void LogWarning (string log)
    {
        Debug.LogWarning (log);
    }
}
