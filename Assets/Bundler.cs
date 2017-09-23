namespace Utilities.Assets
{	
	using System.Collections.Generic;
	using UnityEngine;
	
	//TODO less lazy getting, actually understand Unity's lifecycles
	public sealed class Bundler<T> where T : Bundle, new()
	{		
		private T _bundle;

		private T _Bundle
		{
			get
			{
				_bundle = _bundle ?? new T();
			
				if (!_bundle.IsLoaded())
					_bundle.Load();

				return _bundle;
			}
		}
		
		public Bundler()
		{
			AssetBundle.UnloadAllAssetBundles(true);
			AssignBundleToBehaviors();
			Behavior<T>.Behaviors.OnAfterItemAdd += AssignBundleToBehavior;
		}
		
		public void AssignBundleToBehaviors(IEnumerable<Behavior<T>> behaviors)
		{
			foreach (var behaviour in behaviors)
				AssignBundleToBehavior(behaviour);
		}

		public void AssignBundleToBehaviors()
		{
			AssignBundleToBehaviors(Behavior<T>.Behaviors);
		}
		
		private void AssignBundleToBehavior(Behavior<T> behavior)
		{	
			if (!behavior.HasBundle)
				behavior.SetBundle(_Bundle);
		}
	}
}
