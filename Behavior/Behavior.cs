namespace Utilities
{
	using UnityEngine;
	using Assets;
	
	[ExecuteInEditMode]
	public abstract class Behavior<T> : MonoBehaviour where T : Bundle
	{
		[SerializeField] private T _bundle;
		[SerializeField] private Prefab[] _prefabs;
		
		public static T Bundle { get; set; }
		public static bool HasBundle => Bundle != null;

		protected abstract void SetVisuals(T bundle);

		private void Awake()
		{
			if (Application.isPlaying)
			{
				foreach (var prefab in _prefabs)
				{
					prefab.Instantiate();
				}
			}				
		}

		private void Update()
		{
			if (Bundle != null)
				SetVisuals(Bundle);
			else if (_bundle != null)
				SetVisuals(_bundle);
			
			if (!Application.isPlaying)
				RepopulatePrefabs();		
		}

		private void RepopulatePrefabs()
		{
			if (_prefabs == null)
				return;
			
			foreach (var prefab in _prefabs)
			{
				prefab.DestroyInstances(true);
				prefab.Instantiate();
			}
		}
	}
}
