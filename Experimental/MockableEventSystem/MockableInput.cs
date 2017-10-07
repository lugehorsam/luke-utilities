using UnityEngine;

namespace Utilities
{
	using UnityEngine.EventSystems;
	
	public class MockableInput : BaseInput
	{
		public override int touchCount => GetTouchCountOnEditorUpdate();
		
		public override bool GetMouseButton(int button)
		{
			return true;
		}

		public override Touch GetTouch(int index)
		{	
			Vector3 mousePosition = Event.current.mousePosition;
			var touch = new Touch
			{
				position = mousePosition,
				type = TouchType.Direct,
				phase = TouchPhase.Began,
				fingerId = 0,
				tapCount = 1
			};
			return touch;
		}

		int GetTouchCountOnEditorUpdate()
		{
			if (Event.current == null)
				return 0;
					
			if (Event.current.alt)
				return 1;
			
			return 0;
		}
	}
}
