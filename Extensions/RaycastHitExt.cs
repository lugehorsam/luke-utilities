using UnityEngine;

namespace Utilities
{
	public static class RaycastHitExt 
	{
		public static Controller GetView(this RaycastHit thisHit)
		{
			return thisHit.collider.GetView();
		}
	
	}
}
