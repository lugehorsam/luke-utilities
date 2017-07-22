using UnityEngine;

namespace Utilities.Input
{
	public struct TouchLogic {				
		
		public bool IsPositionChange { get; private set; }
		public bool IsFirstDownOn { get; private set; }
		public bool IsFirstDownOff { get; private set; }

		public bool IsFirstDrag { get; private set; }
		public bool IsRelease { get; private set; }

		public bool IsDownOver { get; private set; }
		public bool WasDownOver { get; private set; }

		public bool IsDrag { get; private set; }
		public bool WasDrag { get; private set; }
			
		private Vector3 _position;
		private Vector3 _lastPosition;	
		
		public void UpdateFrame
		(
			Vector3 position,
			bool isDown,
			bool wasDown,
			bool isRelease,
			bool over
		)
		{			
			_lastPosition = _position;					
			_position = position;
			
			WasDownOver = IsDownOver;
			WasDrag = !isRelease && IsDrag;

			IsPositionChange = _lastPosition != _position;
			IsDownOver = isDown && over;
			IsFirstDownOn = !wasDown && IsDownOver;
			IsFirstDownOff = !wasDown && isDown && !over;
			IsFirstDrag = WasDownOver && isDown && IsPositionChange;
			IsDrag = !isRelease && (IsFirstDrag || WasDrag);
			IsRelease = isRelease && (WasDownOver || WasDrag);
		}
	}	
}
