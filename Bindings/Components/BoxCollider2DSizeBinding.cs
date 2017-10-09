namespace Utilities.Bindings
{
	using UnityEngine;

	[ExecuteInEditMode]
	public class BoxCollider2DSizeBinding : Vector2Binding<BoxCollider2D> 
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
