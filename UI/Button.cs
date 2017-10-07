namespace Utilities.UI
{
	using UnityEngine;

	[ExecuteInEditMode]
	public class Button : UnityEngine.UI.Button
	{
		[SerializeField] private ColorBlockObject _colorBlockObject;

		private void Awake()
		{
			if (_colorBlockObject != null)
			{
				colors = _colorBlockObject.ColorBlock;
			}
		}
	}
}