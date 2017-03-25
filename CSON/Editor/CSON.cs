using UnityEditor;
using UnityEngine;
using System.Linq;

namespace Utilities
{
    [InitializeOnLoad]
    public static class CSON
    {        
        static CSON()
        {
            EditorApplication.playmodeStateChanged += HandlePlaymodeStateChanged;
        }

        static void HandlePlaymodeStateChanged()
        {
            CSONToJSON();
        }

        [MenuItem("Assets/CSON To JSON")]
        public static void CSONToJSON()
        {  
            string csonConfigPath = AssetDatabase.FindAssets("t:CSONConfig").First();
            CSONConfig csonConfig = AssetDatabase.LoadAssetAtPath<CSONConfig>(csonConfigPath);

            string bashScriptName = csonConfig.BashScriptName;
            string csonDirectoryName = csonConfig.CsonDirectoryName;
            string jsonDirectoryName = csonConfig.JsonDirectoryName;

            string csonDirectoryPath = IOExtensions.GetPathToDirectoryFromAssets(csonDirectoryName);
            string jsonDirectoryPath = IOExtensions.GetPathToDirectoryFromAssets(jsonDirectoryName);

            BashScript script = new BashScript
            (
                IOExtensions.GetFullPathToUnityFile(bashScriptName),
                new [] {
                    csonDirectoryPath,
                    jsonDirectoryPath
                },
                Application.dataPath
            );

            try
            {
                script.Run();
            }
            catch (BashScriptException e)
            {
                Debug.LogError(e.Message);
            }
            
            Debug.Log(script.StdOut);
        }
    }
}
