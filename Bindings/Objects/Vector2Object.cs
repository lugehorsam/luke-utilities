namespace Utilities.Bindings
{
	using UnityEngine;

	[CreateAssetMenu]
	public class Vector2Object : PropertyObject<Vector2>
	{
		[SerializeField] private FloatObject _x;
		[SerializeField] private FloatObject _y;

		protected override Vector2 ProcessProperty(Vector2 property)
		{
			return new Vector2
			(
				_x == null ? property.x : _x.Property,
				_y == null ? property.y : _y.Property
			);
		}
	}
}
