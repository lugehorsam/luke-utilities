using UnityEngine;

namespace Utilities
{
	using UnityEngine.EventSystems;
	
	public class MockableInput : BaseInput
	{
		public override int touchCount => 1;
		
		public override bool GetMouseButton(int button)
		{
			return true;
		}

		public override Touch GetTouch(int index)
		{
			
			Vector3 mousePosition = Event.current.mousePosition;
			var touch = new Touch();
			touch.position = mousePosition;
			touch.type = TouchType.Direct;
			touch.phase = TouchPhase.Began;
			touch.fingerId = 0;
			touch.tapCount = 1;
			return touch;
		}

		public override Vector2 mousePosition
		{
			get
			{
				Diag.Log("returning mouse position " + base.mousePosition);
				return base.mousePosition;
			}
		}
	}
}
