namespace Utilities.Bindings
{
	using UnityEngine;

	[CreateAssetMenu]
	public class FontObject : PropertyObject<Font>
	{
		public override BindType BindType => BindType.Font;

	}
}
