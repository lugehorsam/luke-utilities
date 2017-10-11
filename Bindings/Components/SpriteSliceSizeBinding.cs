namespace Utilities.Bindings
{
	using UnityEngine;

	[ExecuteInEditMode]
	public class SpriteSliceSizeBinding : Vector3Binding<SpriteRenderer> 
	{	
		public override Vector3 GetProperty()
		{
			return _Component.size;
		}

		public override void SetProperty(Vector3 property)
		{
			_Component.size = property;
		}
	}
}
