#define DEBUG_LOG

namespace Utilities
{
    using System;
    using System.Reflection;
    using System.Text;

    using UnityEngine;

    public static class Diag
    {
        private const float GESTURE_DRAW_DURATION = 1000f; //in seconds

        private static string text;
        private static TextAnchor textAlignment;
        private static Color color;

        private static bool AllLogsEnabled
        {
            get { return CurrentLogType is UtilitiesFeature && ((UtilitiesFeature) CurrentLogType == UtilitiesFeature.All); }
        }

        public static Enum CurrentLogType { get; set; } = UtilitiesFeature.All;

        public static string Reflect(object obj)
        {
            Type objType = obj.GetType();

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Type Name: {objType.Name}");

            foreach (FieldInfo field in objType.GetFields())
            {
                stringBuilder.AppendLine($"Field Name: {field.Name}, Field Value: {field.GetValue(obj)}");
            }

            foreach (PropertyInfo prop in objType.GetProperties())
            {
                stringBuilder.AppendLine($"Property Name: {prop.Name}, Property Value: {prop.GetValue(obj)}");
            }

            return stringBuilder.ToString();
        }

        public static void Report(string message)
        {
            Debug.LogError(message);
        }

        public static void Report(Exception e)
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

        public static void Log(Enum feature, string log)
        {
            if (CurrentLogType == null)
            {
                Warn("No current log type is assigned. Logging is disabled.");
                return;
            }

            if (AllLogsEnabled || (CurrentLogType == feature))
            {
                Log($"[{feature}]:{log}");
            }
        }

        public static void Warn(string log)
        {
            Debug.LogWarning(log);
        }
    }
}
