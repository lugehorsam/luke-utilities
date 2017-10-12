namespace Utilities.Bindings
{
	using System;
	using UnityEngine;
	
	public class PropertyConfig : MonoBehaviour
	{
		[SerializeField] private PropertyConfigData[] _configData;

		private void Awake()
		{
			foreach (var configDatum in _configData)
			{
				configDatum.Resolve();	
			}
		}

		[Serializable]
		private class PropertyConfigData
		{
			[SerializeField] private ScriptableObject _object;
			[SerializeField] private MonoBehaviour _binding;
			
			public void Resolve()
			{
				IPropertyBinding binding = _binding as IPropertyBinding;
				binding.SetObject(_object);
			}
		}
	}	
}
