namespace Utilities
{
    using System;
    using System.IO;

    using UnityEditor;

    using UnityEngine;

    using Object = UnityEngine.Object;

    public static class ScriptableObjectFactory
    {
        [MenuItem("Assets/Create/Scriptable Object")]
        public static void CreateInstance()
        {
            foreach (Object o in Selection.objects)
            {
                if (o is MonoScript)
                {
                    var script = (MonoScript) o;
                    Type type = script.GetClass();
                    if (type.IsSubclassOf(typeof(ScriptableObject)))
                    {
                        CreateAsset(type);
                    }
                }
            }
        }

        [MenuItem("Assets/Create/Scriptable Object", true)]
        public static bool ValidateCreateInstance()
        {
            foreach (Object o in Selection.objects)
            {
                if (o is MonoScript)
                {
                    var script = (MonoScript) o;
                    Type type = script.GetClass();
                    if (type.IsSubclassOf(typeof(ScriptableObject)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static void CreateAsset(Type type)
        {
            ScriptableObject asset = ScriptableObject.CreateInstance(type);
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + type + ".asset");
            AssetDatabase.CreateAsset(asset, assetPathAndName);
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }
}
