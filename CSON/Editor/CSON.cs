using UnityEditor;
using UnityEngine;

namespace Utilities
{
    [InitializeOnLoad]
    public static class CSON
    {
        private const string BASH_SCRIPT_NAME = "CSON.sh";
        private const string CSON_PATH = "/CSON/";
        private const string JSON_PATH = "/Resources/JSON/";
        private static bool TRANSPILE_ON_PLAY = false;

        static CSON()
        {
            EditorApplication.playmodeStateChanged += HandlePlaymodeStateChanged;
        }

        static void HandlePlaymodeStateChanged()
        {
            if (TRANSPILE_ON_PLAY)
                CSONToJSON();
        }

        [MenuItem("Assets/CSON To JSON")]
        public static void CSONToJSON()
        {
            BashScript script = new BashScript
            (
                IOExtensions.GetFullPathToUnityFile(BASH_SCRIPT_NAME),
                new [] {
                    Application.dataPath + CSON_PATH,
                    Application.dataPath + JSON_PATH
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