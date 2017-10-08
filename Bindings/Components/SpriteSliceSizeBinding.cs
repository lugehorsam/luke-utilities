namespace Utilities.Bindings
{
	using UnityEngine;

	[ExecuteInEditMode]
	public class SpriteSliceSizeBinding : Vector2Binding<SpriteRenderer> 
	{
		public override Vector2 GetProperty()
		{
			return _Component.size;
		}

		public override void SetProperty(Vector2 property)
		{
			_Component.size = property;
		}
	}
}
