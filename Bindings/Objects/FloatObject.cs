namespace Utilities.Bindings
{
	using UnityEngine;
	
	[CreateAssetMenu]
	public class FloatObject : PropertyObject<float> 
	{
		public override BindType BindType => BindType.Float;

	}	
}
