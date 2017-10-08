namespace Utilities.Bindings
{
	using UnityEngine;
	
	[CreateAssetMenu]
	public class PrefabObject : ScriptableObject
	{
		[SerializeField] private Prefab _prefab;	
	}
}
