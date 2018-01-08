namespace TouchDispatch
{
    using UnityEngine;
    using Utilities;
    
    public class TouchLogic
    {
        public bool IsPositionChange { get; private set; }

        public bool IsFirstDown { get; private set; }
        public bool IsFirstDownOff { get; private set; }
        public bool WasFirstDown { get; private set; }

        public bool IsFirstDrag { get; private set; }
        public bool IsRelease { get; private set; }

        public bool IsDown { get; private set; }
        public bool WasDown { get; private set; }

        public bool IsDrag { get; private set; }
        public bool WasDrag { get; private set; }

        public bool IsHold { get; private set; }
        public bool WasHold { get; private set; }

        private Vector3 _position;
        private Vector3 _lastPosition;

        public void UpdateFrame(Vector3 position, bool isDown, bool wasDown, bool isRelease, bool over)
        {
            _lastPosition = _position;
            _position = position;

            IsPositionChange = _lastPosition != _position;
            IsDown = isDown && over;
            IsFirstDown = !wasDown && IsDown;

            IsFirstDownOff = !wasDown && isDown && !over;
            IsHold = (IsFirstDown || WasHold) && !isRelease;
            IsFirstDrag = IsHold && IsPositionChange;
            IsDrag = !isRelease && (IsFirstDrag || WasDrag) && IsPositionChange;

            IsRelease = isRelease && (WasDrag || WasHold);

            WasDown = IsDown;
            WasFirstDown = IsFirstDown;
            WasHold = IsHold;
            WasDrag = !isRelease && IsDrag;
        }

        public void ForceDragMode()
        {
            WasDown = true;
            WasHold = true;
            WasDrag = true;
        }
    }
}
