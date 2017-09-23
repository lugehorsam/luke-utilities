using System.Linq;

namespace Utilities.Assets
{
	using System.IO;
	using UnityEngine;
	
	public abstract class Bundle
	{
		protected AssetBundle _assetBundle;

		public static string Directory => Path.Combine(Application.streamingAssetsPath, "AssetBundles");
				
		public abstract string BundleId { get; }

		public void Load()
		{
			_assetBundle = _assetBundle ?? AssetBundle.LoadFromFile(Path.Combine(Directory, BundleId));
		}

		public bool IsLoaded()
		{
			var loadedBundles = AssetBundle.GetAllLoadedAssetBundles();
			return loadedBundles.Any(bundle => bundle.name == BundleId);
		}
	}
}
