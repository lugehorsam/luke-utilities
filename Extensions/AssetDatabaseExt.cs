namespace Utilities
{
	using System.Linq;
	using System.Collections.Generic;
	using UnityEditor;
	using UnityEngine;
	
	public class AssetDatabaseExt
	{
		public static IEnumerable<T> GetAssetsOfType<T>() where T : Object
		{
			var guids = AssetDatabase.FindAssets("t: " + typeof(T));		
			var paths = guids.Select(AssetDatabase.GUIDToAssetPath);
			var assets = paths.Select(AssetDatabase.LoadAssetAtPath<T>);
			return assets;
		}	
	}
}
