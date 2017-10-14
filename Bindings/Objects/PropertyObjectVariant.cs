namespace Utilities.Bindings
{
	using System;
	using UnityEngine;
	
	[Serializable]
	public class PropertyObjectVariant
	{
		[SerializeField] private string id;
		[SerializeField] private ScriptableObject _object;

		public string Id => id;
		public ScriptableObject Object => _object;
	}
}
