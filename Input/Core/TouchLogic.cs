using UnityEngine;

namespace Utilities.Input
{
	public struct TouchLogic {
		
		public bool IsPositionChange { get; private set; }

		public bool IsFirstDown { get; private set; }
		
		public bool IsDown { get; private set; }
		public bool WasDown { get; private set; }
		
		public bool IsDownOver { get; private set; }
		public bool WasDownOver { get; private set; }


		public bool IsDrag { get; private set; }
		public bool WasDrag { get; private set; }
		
		public bool IsRelease { get; private set; }
		public bool WasRelease { get; private set; }
			
		private Vector3 _position;
		private Vector3 _lastPosition;	
		
		public void UpdateFrame
		(
			Vector3 mousePosition,
			bool mouseDown,
			bool isOver
		)
		{			
			_lastPosition = _position;					
			_position = mousePosition;
			
			WasDown = IsDown;
			WasDownOver = IsDownOver;
			WasDrag = IsDrag;
			WasRelease = IsRelease;

			IsPositionChange = _lastPosition != _position;
			IsDown = mouseDown;
			IsDown = mouseDown && isOver;
			IsFirstDown = !WasDown && IsDown;
			IsDrag = IsPositionChange && WasDown && IsDownOver;			
			IsRelease = WasDown && !IsDownOver;
		}	
	}	
}
