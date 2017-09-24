namespace Utilities
{
	using UnityEngine;
	using Assets;
	
	[ExecuteInEditMode]
	public abstract class Visuals<T> : MonoBehaviour where T : Bundle
	{
		[SerializeField] private T _bundle;
		
		public static T Bundle { get; set; }
		public static bool HasBundle => Bundle != null;

		protected abstract void SetVisuals(T bundle);

		private void Update()
		{
			if (Bundle != null)
				SetVisuals(Bundle);
			else if (_bundle != null)
				SetVisuals(_bundle);			
		}
	}
}
