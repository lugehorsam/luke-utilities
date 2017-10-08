namespace Utilities.UI
{
	using UnityEngine;
	
	[ExecuteInEditMode]
	public class Image : UnityEngine.UI.Image {

		[SerializeField] private ColorObject _colorObject;

		private void Awake()
		{
			if (_colorObject != null)
			{
				color = _colorObject.Property;
			}
		}

		public void OnGUI()
		{
			if (_colorObject != null)
				color = _colorObject.Property;
		}
	}
}
