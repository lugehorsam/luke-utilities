namespace Utilities.Bindings
{
	using UnityEngine;

	[ExecuteInEditMode]
	public class BoxCollider2DSizeBinding : Vector3Binding<BoxCollider2D> 
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
