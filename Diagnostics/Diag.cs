#define DEBUG_LOG

using System.Text;

namespace Utilities
{
    using System;
    using System.Reflection;
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

        public static string Reflect(object obj)
        {
            Type objType = obj.GetType();
                       
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Type Name: {objType.Name}");

            foreach (var field in objType.GetFields())
            {
                stringBuilder.AppendLine($"Field Name: {field.Name}, Field Value: {field.GetValue(obj)}");
            }
            
            foreach (var prop in objType.GetProperties())
            {
                stringBuilder.AppendLine($"Property Name: {prop.Name}, Property Value: {prop.GetValue(obj)}");
            }
            
            return stringBuilder.ToString();
        }
    
        public static void Report(string message)
        {
            Debug.LogError(message);            
        }
    
        public static void Report (Exception e)
        {
            Report(e.Message);
        }

        public static void Crumb(object obj, string crumb)
        {
            string typeName = obj.GetType().Name;
            
            Debug.Log($"[{typeName}] {crumb}");
        }
    
        public static void Log(string log)
        {
            Debug.Log(log);
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
            Debug.LogWarning (log);
        }        
    }
}