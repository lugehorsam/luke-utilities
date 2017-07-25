using UnityEngine;

namespace Utilities
{
    public class CSONConfig : ScriptableObject
    {
        public const string DEFAULT_BASH_SCRIPT_NAME = "CSON.sh";
        public const string DEFAULT_CSON_DIRECTORY_NAME = "CSON";
        public const string DEFAULT_JSON_DIRECTORY_NAME = "JSON";

        public bool Enabled => enabled;

        [SerializeField] private bool enabled = false;

        public string CSONDirectoryPathFromAssets => IOExtensions.GetPathToDirectoryFromAssets(CsonDirectoryName) + "/";

        public string BashScriptPathFromAssets => IOExtensions.GetPathToFileFromAssets(BashScriptName);

        protected virtual string BashScriptName => DEFAULT_BASH_SCRIPT_NAME;

        protected virtual string CsonDirectoryName => DEFAULT_CSON_DIRECTORY_NAME;

        public string JSONDirectoryPathFromAssets => IOExtensions.GetPathToDirectoryFromAssets(JsonDirectoryName) + "/";

        protected virtual string JsonDirectoryName => DEFAULT_JSON_DIRECTORY_NAME;
    }
}
