using UnityEngine;

namespace Utilities.Input
{
	public class TouchEventInfo : ITouchEventInfo
	{
		public GameObject Owner { get; }
		public ITouchState TouchState { get; }
		public RaycastHit[] Hits { get; }
		public RaycastHit2D[] Hits2D { get; }
		public Vector3 WorldPosition { get; }
		public Dimension Dimension { get; }

		public TouchEventInfo(TouchDispatcher dispatcher, TouchLogic touchState, RaycastHit[] hits, Vector3 worldPosition)
		{
			Owner = dispatcher.gameObject;
			TouchState = touchState;
			Hits = hits;
			WorldPosition = worldPosition;
			Dimension = Dimension.Three;
		}
		
		public TouchEventInfo(TouchDispatcher dispatcher, TouchLogic touchState, RaycastHit2D[] hits, Vector3 worldPosition)
		{
			Owner = dispatcher.gameObject;
			TouchState = touchState;
			Hits2D = hits;
			WorldPosition = worldPosition;
			Dimension = Dimension.Two;
		}
		
		
		
	}
}
