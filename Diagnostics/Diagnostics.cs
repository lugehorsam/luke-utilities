using UnityEngine;
using System.Reflection;
using System;
using System.Collections.Generic;

public static class Diagnostics {

    const float GESTURE_DRAW_DURATION = 1000f; //in seconds

    static string text;
    static TextAnchor textAlignment; 
    static Color color;

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

    public static void Report(string message, params string[] formatters)
    {
        if (formatters == null)
            Debug.LogError(message);
        else
            Debug.LogError(string.Format(message, formatters));
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

    public static void Log (string log, params object[] parameters)
    {
        Debug.Log(string.Format(log, parameters));
    }


    public static void LogWarning (string log, params string[] inserts)
    {
        Debug.LogWarning (string.Format(log, inserts));
    }
    
    public static void LogWarning (string log)
    {
        Debug.LogWarning (log);
    }
    
    public static void DrawGesture(Gesture gesture)
    {
        for (int frameIndex = 0; frameIndex < gesture.GestureFrames.Count - 1; frameIndex++)
        {
            GestureFrame currFrame = gesture.GestureFrames[frameIndex];
            GestureFrame nextFrame = gesture.GestureFrames[frameIndex + 1];
            Debug.DrawLine(currFrame.Position, nextFrame.Position, Color.red, GESTURE_DRAW_DURATION, depthTest: false);
        }
    }

    public static void Display(string text, TextAnchor textAlignment, Color color)
    {
        Diagnostics.text = text;
        Diagnostics.textAlignment = textAlignment;
        Diagnostics.color = color;  
    }

    static void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.normal.textColor = color;
        int w = Screen.width, h = Screen.height;
        Rect guiRect = new Rect(30, 30, w, h);
        guiStyle.alignment = TextAnchor.UpperLeft;
        guiStyle.fontSize = h * 2 / 100;
        GUI.Label(guiRect, text, guiStyle);
    }
}
