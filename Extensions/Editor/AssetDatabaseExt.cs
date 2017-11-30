namespace Utilities
{
    using System.Collections.Generic;
    using System.Linq;

    using UnityEditor;

    using UnityEngine;

    public class AssetDatabaseExt
    {
        public static IEnumerable<T> GetAssetsOfType<T>() where T : Object
        {
            string[] guids = AssetDatabase.FindAssets("t: " + typeof(T));
            IEnumerable<string> paths = guids.Select(AssetDatabase.GUIDToAssetPath);
            IEnumerable<T> assets = paths.Select(AssetDatabase.LoadAssetAtPath<T>);
            return assets;
        }
    }
}
