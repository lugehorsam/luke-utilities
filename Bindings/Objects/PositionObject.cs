namespace Utilities.Bindings
{
	using UnityEngine;
	
	[CreateAssetMenu]
	public sealed class PositionObject : Vector3Object 
	{
		[SerializeField] private Vector3Object _sizeObject;
		[SerializeField] private Vector3Object _pivot;
		[SerializeField] private Vector3Object _anchor;

		private bool _ShouldApplyPivot => _sizeObject != null && _pivot != null;
		private bool _ShouldApplyAnchor => _anchor != null;

		protected override Vector3 ProcessProperty(Vector3 property)
		{
			property = base.ProcessProperty(property);
			
			Diag.Log("property before anchor " + property);

			if (_ShouldApplyAnchor)
			{
				Vector3 worldAnchoredPosition = Camera.main.ScreenToWorldPoint(_anchor.Property);
				property += worldAnchoredPosition;
			}
			
			Diag.Log("property after anchor " + property);

			
			if (_ShouldApplyPivot)
			{
				property += new Vector3
				(
					_sizeObject.Property.x * _pivot.Property.x,
					_sizeObject.Property.y * _pivot.Property.y,
					_sizeObject.Property.z * _pivot.Property.z
				);
			}
			
			Diag.Log("property after pivot " + property);


			return property;
		}
	}
}