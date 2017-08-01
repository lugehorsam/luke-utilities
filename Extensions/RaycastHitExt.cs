using UnityEngine;

namespace Utilities
{
	public static class RaycastHitExt 
	{
		public static View GetView(this RaycastHit thisHit)
		{
			return thisHit.collider.GetView();
		}
	
	}
}
