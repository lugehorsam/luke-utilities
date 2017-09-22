namespace Utilities.Assets
{
	using System.IO;
	using UnityEngine;
	
	public abstract class Bundle : ScriptableObject
	{
		protected AssetBundle _assetBundle;

		public static string Directory => Path.Combine(Application.streamingAssetsPath, "AssetBundles");
				
		public abstract string BundleId { get; }

		public void Load()
		{
			_assetBundle = _assetBundle ?? AssetBundle.LoadFromFile(Path.Combine(Directory, BundleId));
		}
		
#if UNITY_EDITOR
		public void ReassignReference(AssetBundle assetBundle)
		{
			_assetBundle = assetBundle;
		}
#endif
	}
}
