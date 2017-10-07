namespace Utilities
{
	using UnityEngine;

	public class Instantiator : MonoBehaviour
	{
		[SerializeField] private Prefab[] _prefabs;

		private void Awake()
		{
			foreach (var prefab in _prefabs)
			{
				prefab.Instantiate();
			}
		}
	}	
}
