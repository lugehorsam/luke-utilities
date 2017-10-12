namespace Utilities.Bindings
{
	using System;
	using UnityEngine;
	
	public class PropertyConfigs : MonoBehaviour
	{
		[SerializeField] private PropertyConfigVariant[] _configVariants;
		
		[Serializable]
		private class PropertyConfigVariant
		{
			[SerializeField] private string _name;
			[SerializeField] private PropertyConfig _config;
		}
	}
}
