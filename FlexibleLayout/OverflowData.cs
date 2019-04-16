namespace Utilities
{
	using UnityEngine;

	public class OverflowData
	{
		private GameObject overflowObject;
		private float overflowAmount;

		public GameObject OverflowObject => overflowObject;
		public float OverflowAmount => overflowAmount;

		public OverflowData(GameObject overflowObject, float overflowAmount)
		{
			this.overflowObject = overflowObject;
			this.overflowAmount = overflowAmount;
		}
	}
}
