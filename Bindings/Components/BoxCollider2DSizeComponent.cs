namespace Utilities.Bindings
{
	using UnityEngine;

	[ExecuteInEditMode]
	public class BoxCollider2DSizeComponent : Vector3Component<BoxCollider2D> 
	{	
		public override BindType BindType => BindType.ColliderSize;

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
