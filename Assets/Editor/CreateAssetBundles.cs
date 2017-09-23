namespace Utilities.Assets
{
	using System.IO;
	using UnityEditor;

	/**
	public class CreateAssetBundles
	{	
		[MenuItem("Assets/Build Asset Bundles")]
		static void BuildAllAssetBundles()
		{
			if (!Directory.Exists(Bundle.Directory))
			{
				Directory.CreateDirectory(Bundle.Directory);
			}
		
			BuildPipeline.BuildAssetBundles(Bundle.Directory, BuildAssetBundleOptions.None, BuildTarget.StandaloneOSXUniversal);
		}
	}**/
}
