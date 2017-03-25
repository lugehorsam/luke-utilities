using UnityEngine;

namespace Utilities
{
    public class CSONConfig : ScriptableObject
    {
        public const string DEFAULT_BASH_SCRIPT_NAME = "CSON.sh";
        public const string DEFAULT_CSON_DIRECTORY_NAME = "CSON";
        public const string DEFAULT_JSON_DIRECTORY_NAME = "JSON";
        
        public virtual string BashScriptName
        {
            get { return DEFAULT_BASH_SCRIPT_NAME; }
        }

        public virtual string CsonDirectoryName
        {
            get { return DEFAULT_CSON_DIRECTORY_NAME; }
        }

        public virtual string JsonDirectoryName
        {
            get { return DEFAULT_JSON_DIRECTORY_NAME; }
        }
    }
}
