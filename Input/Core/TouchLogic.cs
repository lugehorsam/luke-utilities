using UnityEngine;

namespace Utilities.Input
{
	public struct TouchLogic : ITouchState {				
		
		public bool IsPositionChange { get; private set; }
		public bool IsFirstDownOn { get; private set; }
		public bool IsFirstDownOff { get; private set; }
		public bool WasFirstDownOn { get; private set; }

		public bool IsFirstDrag { get; private set; }
		public bool IsRelease { get; private set; }

		public bool IsDownOn { get; private set; }
		public bool WasDownOn { get; private set; }

		public bool IsDrag { get; private set; }
		public bool WasDrag { get; private set; }
		
		public bool IsHold { get; private set; }
		public bool WasHold { get; private set; }
			
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

			IsPositionChange = _lastPosition != _position;
			IsDownOn = isDown && over;
			IsFirstDownOn = !wasDown && IsDownOn;
			IsFirstDownOff = !wasDown && isDown && !over;
			IsHold = (IsFirstDownOn || WasHold) && !isRelease;
			IsFirstDrag = IsHold && IsPositionChange;
			IsDrag = !isRelease && (IsFirstDrag || WasDrag);
			
			IsRelease = isRelease && WasDrag;
			
			WasDownOn = IsDownOn;
			WasFirstDownOn = IsFirstDownOn;
			WasHold = IsHold;
			WasDrag = !isRelease && IsDrag;
		}
	}	
}
