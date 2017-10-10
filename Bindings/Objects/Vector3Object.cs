namespace Utilities.Bindings
{
	using UnityEngine;

	[CreateAssetMenu]
	public class Vector3Object : PropertyObject<Vector3>
	{
		[SerializeField] private FloatObject _x;
		[SerializeField] private FloatObject _y;
		[SerializeField] private FloatObject _z;

		protected override Vector3 ProcessProperty(Vector3 property)
		{
			return new Vector3
			(
				_x == null ? property.x : _x.Property,
				_y == null ? property.y : _y.Property,
				_z == null ? property.z : _z.Property
			);
		}
	}
}
