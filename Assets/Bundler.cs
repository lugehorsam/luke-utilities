namespace Utilities.Assets
{	
	using UnityEngine;
	
	public sealed class Bundler<T> where T : Bundle, new()
	{
		private readonly T _bundle;

		public Bundler()
		{
			AssetBundle.UnloadAllAssetBundles(true);
			_bundle = new T();
			_bundle.Load();
			AssignBundleToBehaviors();
			Behavior<T>.Behaviors.OnAfterItemAdd += AssignBundleToBehavior;
		}
		
		public void AssignBundleToBehaviors()
		{
			foreach (var behaviour in Behavior<T>.Behaviors)
				AssignBundleToBehavior(behaviour);
		}
		
		private void AssignBundleToBehavior(Behavior<T> behavior)
		{
			if (!behavior.HasBundle)
				behavior.SetBundle(_bundle);

		}
	}
}
