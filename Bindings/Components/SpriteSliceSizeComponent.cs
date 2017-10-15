namespace Utilities.Bindings
{
	using UnityEngine;

	public class SpriteSliceSizeComponent : Vector3Component<SpriteRenderer> 
	{	
		public override BindType BindType => BindType.SpriteSize;

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
