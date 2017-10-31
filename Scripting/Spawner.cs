namespace Utilities
{
	using System.Collections;
	
	using UnityEngine;

	public class Spawner : MonoBehaviour
	{
		[SerializeField] private float _spawnInterval;
		[SerializeField] private Prefab _prefab;
		
		private IEnumerator Start()
		{

			_prefab.Instantiate(transform);
			yield return new WaitForSeconds(_spawnInterval);
		}
	}	
}
