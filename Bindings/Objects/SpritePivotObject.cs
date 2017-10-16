namespace Utilities.Bindings
{
	using UnityEngine;

	[CreateAssetMenu]
	public class SpritePivotObject : Vector3Object
	{
		public override BindType BindType => BindType.SpritePivot;
	}
}
