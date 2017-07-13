using UnityEngine;

namespace Utilities.Input
{
	public class TouchEventInfo {

		public TouchDispatcher TouchDispatcher { get; }
		public TouchLogic Logic { get; }
		public RaycastHit[] Hits { get; }

		public TouchEventInfo(TouchDispatcher dispatcher, TouchLogic logic, RaycastHit[] hits)
		{
			TouchDispatcher = dispatcher;
			Logic = logic;
			Hits = hits;
		}
	}
}

