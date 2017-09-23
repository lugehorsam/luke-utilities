using System.Linq;

namespace Utilities.Assets
{
	using UnityEngine;
	
	public class Bundle : ScriptableObject
	{
		[SerializeField] private string _bundleId;
		public string BundleId => _bundleId;
		
		// TODO implement for asset bundles
		public void Load()
		{
			
		}

		public bool IsLoaded()
		{
			var loadedBundles = AssetBundle.GetAllLoadedAssetBundles();
			return loadedBundles.Any(bundle => bundle.name == BundleId);
		}
	}
}
