#if UNITY_EDITOR

namespace Utilities.Assets
{
	using System.Linq;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEditor;

	/// <summary>
	/// Ensures all asset bundles are loaded at once in editor and  that all instances of <see cref="Bundle"/> 
	/// in the project maintain their reference to their asset bundle across play and compilation phases.
	/// </summary>
	[InitializeOnLoad]
	public static class EditorBundles
	{
		private static IEnumerable<AssetBundle> _assetBundles;
		private static IEnumerable<Bundle> _bundles;
		
		static EditorBundles()
		{
			LoadAllAssetBundles();
			LoadAllBundles();
			EditorApplication.update += OnEditorUpdate;
		}
		
		
		private static void OnEditorUpdate()
		{
			KeepBundlesUpdated();
		}

		private static void KeepBundlesUpdated()
		{
			foreach (var bundle in _bundles)
			{
				var associatedAssetBundle = _assetBundles.FirstOrDefault(assetBundle => bundle.BundleId == assetBundle.name);
				bundle.ReassignReference(associatedAssetBundle);
			}
		}

		private static void LoadAllAssetBundles()
		{
			var assetBundleNames = AssetDatabase.GetAllAssetBundleNames();
			_assetBundles = assetBundleNames.Select(AssetBundle.LoadFromFile);
		}

		private static void LoadAllBundles()
		{
			var bundleGUIDs = AssetDatabase.FindAssets("t: " + typeof(Bundle));		
			var paths = bundleGUIDs.Select(AssetDatabase.GUIDToAssetPath);
			var bundles = paths.Select(AssetDatabase.LoadAssetAtPath<Bundle>);
			_bundles = bundles;
		}
	}
}

#endif