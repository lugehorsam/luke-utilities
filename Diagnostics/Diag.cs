#define DEBUG_LOG

namespace Utilities
{
    using System;
    using UnityEngine;
    
    public static class Diag {
    
        private static bool AllLogsEnabled => CurrentLogType is UtilitiesFeature &&
                                              (UtilitiesFeature) CurrentLogType == UtilitiesFeature.All;
        
        const float GESTURE_DRAW_DURATION = 1000f; //in seconds
    
        static string text;
        static TextAnchor textAlignment; 
        static Color color;
    
        public static Enum CurrentLogType {
            get {
                return currentLogType;
            }
            set {
                currentLogType = value;
            }
        }
    
        static Enum currentLogType = UtilitiesFeature.All; //override here
    
        public static void Report(string message)
        {
            #if LOCAL_LOGS
                Debug.LogError(message);
            #endif
        }
    
        public static void Report (Exception e)
        {
            Report(e.Message);
        }
    
        public static void Log(string log)
        {
#if DEBUG_LOG
                Debug.Log(log);
#endif
        }
        
        public static void Log (Enum feature, string log)
        {
            if (CurrentLogType == null)
            {
                Warn("No current log type is assigned. Logging is disabled.");
                return;
            }
            
            if (AllLogsEnabled || CurrentLogType == feature) {
                Log ($"[{feature}]:{log}");
            }
        }
       
        public static void Warn (string log)
        {
#if DEBUG_LOG
            Debug.LogWarning (log);
#endif
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
            Diag.text = text;
            Diag.textAlignment = textAlignment;
            Diag.color = color;  
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
       

}