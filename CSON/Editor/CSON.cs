using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace Utilities
{
    [InitializeOnLoad]
    public static class CSON
    {
        private static readonly string bashScriptPathFromAssets;
        private static readonly string csonDirectoryPathFromassets;
        private static readonly string jsonDirectoryPathFromAssets;
        private static readonly string appDataPath;

        private static readonly FileSystemWatcher fileWatcher;
        
        static CSON()
        {            
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
            Environment.SetEnvironmentVariable("MONO_MANAGED_WATCHER", "enabled");
#endif
            var config = AssetDatabase.FindAssets("CSONConfig").FirstOrDefault();

            string csonConfigPath = AssetDatabase.GUIDToAssetPath(config);                                    
            CSONConfig csonConfig = AssetDatabase.LoadAssetAtPath<CSONConfig>(csonConfigPath);

            if (!csonConfig.Enabled)
                return;
            
            fileWatcher = new FileSystemWatcher(Path.Combine(Application.dataPath, csonConfig.CSONDirectoryPathFromAssets), "*.cson");           
            fileWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.LastAccess;
            fileWatcher.IncludeSubdirectories = true;
            
            fileWatcher.Changed += HandleCSONFileChanged;
            fileWatcher.Created += HandleCSONFileChanged;
            fileWatcher.Deleted += HandleCSONFileChanged;
            fileWatcher.Renamed += HandleCSONFileChanged;           
            fileWatcher.EnableRaisingEvents = true;

            bashScriptPathFromAssets = csonConfig.BashScriptPathFromAssets;
            csonDirectoryPathFromassets = csonConfig.CSONDirectoryPathFromAssets;
            jsonDirectoryPathFromAssets = csonConfig.JSONDirectoryPathFromAssets;
            appDataPath = Application.dataPath;

        }

        static void HandleCSONFileChanged(object sender, FileSystemEventArgs e)
        {
            Diagnostics.Log("File changed");
            CSONToJSON();
        }

        [MenuItem("Assets/CSON To JSON")]
        public static void CSONToJSON()
        {
            Diagnostics.Log("Transpiling CSON");
            BashScript bashScript = new BashScript
            (
                bashScriptPathFromAssets,
                new[]
                {
                    csonDirectoryPathFromassets,
                    jsonDirectoryPathFromAssets
                },
                appDataPath
            );
            
            try
            {
                bashScript.Run();
            }
            catch (BashScriptException e)
            {
                Debug.LogError(e.Message);
            }
            Debug.Log(bashScript.StdOut);
        }
    }
}
