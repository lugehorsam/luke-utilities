using System.Collections.Generic;

namespace Utilities.Bindings
{
	using UnityEngine;

	[CreateAssetMenu]
	public sealed class PositionObject : Vector3Object
	{
		[SerializeField] private Vector3Object _anchor;

		public override BindType BindType => BindType.Position;

		protected override IEnumerable<PropertyObject> ObjectsToWatch => new []
		{
			_anchor
		};
		
		private bool _ShouldApplyAnchor => _anchor != null;

		protected override Vector3 ProcessProperty(Vector3 property)
		{
			property = base.ProcessProperty(property);
			
			if (_ShouldApplyAnchor)
			{
				Vector3 worldAnchoredPosition = Camera.main.ViewportToWorldPoint
				(
					new Vector2(_anchor.Property.x, _anchor.Property.y)				
				);
			
				worldAnchoredPosition.z = property.z;
				
				property = worldAnchoredPosition;
			}			
			
			return property;
		}
	}
}