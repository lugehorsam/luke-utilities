using UnityEngine;

namespace Utilities.Input
{
	public static class GlobalTouchDispatcher
	{
		private static bool _mouseWasDown = false;
		
		public static void Init(LifecycleDispatcher dispatcher)
		{
			dispatcher.OnUpdate += OnUpdate;
		}
		
		public static void OnUpdate()
		{
			_mouseWasDown = UnityEngine.Input.GetMouseButton(0);
		}
	}
}

