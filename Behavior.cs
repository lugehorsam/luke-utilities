namespace Utilities
{
	using UnityEngine;
	
	[ExecuteInEditMode]
	public abstract class Behavior : MonoBehaviour
	{
		protected abstract void SetVisuals();

		public void OnRenderObject()
		{
			SetVisuals();
		}
	}
}