using Utilities.Bindings;

namespace Utilities.UI
{
	using UnityEngine;
	
	[ExecuteInEditMode]
	public class Image : UnityEngine.UI.Image {
/**
		[SerializeField] private ColorProperty<Image> _colorBinding;

		private void Awake()
		{
			if (_colorBinding != null)
			{
				color = _colorBinding.Property;
			}
		}

		public void OnGUI()
		{
			if (_colorBinding != null)
				color = _colorBinding.Property;
		}**/
	}
}
