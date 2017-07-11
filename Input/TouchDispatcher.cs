namespace Utilities.Input
{
    using System;
    using UnityEngine;

    public class TouchDispatcher : MonoBehaviour
    {
        public BoxCollider BoxCollider
        {
            get { return _boxCollider; }
        }
        
        private BoxCollider _boxCollider;
        private Rigidbody _rigidbody;

        public event Action<TouchDispatcher, Gesture> OnTouch = (arg1, arg2) => { };
        public event Action<TouchDispatcher, Gesture> OnHold = (arg1, arg2) => { };
        public event Action<TouchDispatcher, Gesture> OnRelease = (arg1, arg2) => { };
        public event Action<TouchDispatcher, Gesture> OnDrag = (arg1, arg2) => { };
        public event Action<TouchDispatcher, Gesture> OnDragLeave = (arg1, arg2) => { };

        protected virtual void HandleOnTouch(TouchDispatcher touchDispatcher, Gesture gestureFrame) { }
        protected virtual void HandleOnHold(TouchDispatcher touchDispatcher, Gesture gestureFrame) { }
        protected virtual void HandleOnRelease(TouchDispatcher touchDispatcher, Gesture gestureFrame) { }
        protected virtual void HandleOnDrag(TouchDispatcher touchDispatcher, Gesture gestureFrame) { }
        protected virtual void HandleOnDragLeave(TouchDispatcher touchDispatcher, Gesture gestureFrame) { }
        
        
        /// <summary>
        /// May be null if there is no touch input.
        /// </summary>
        public Gesture CurrentGesture 
        {
            get
            {
                return currentGesture;
            }
        }

        private Gesture currentGesture;

        public void Init(Vector3 colliderSize)
        {            
            _boxCollider = gameObject.AddComponent<BoxCollider>();
            _boxCollider.size = colliderSize;
            
            _rigidbody = gameObject.AddComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;           
        }
        
        void Update()
        {
            if (_boxCollider == null)
            {
                Diagnostics.LogWarning("Touch dispatcher added to " + name + " with no _boxCollider2D");
                return;
            }           
           
            bool mouseIsDown = Input.GetMouseButton(0);
            bool mouseWasDown = currentGesture != null;

            RaycastHit? hitInfo = GetHitInfo(Input.mousePosition);

            bool firstTouch = !mouseWasDown && mouseIsDown && hitInfo.HasValue;
            bool hold = mouseWasDown && mouseIsDown;
            bool release = mouseWasDown && !mouseIsDown;
            bool drag = hold && currentGesture.MousePositionCurrent != currentGesture.MousePositionLast;
            
            if (firstTouch)
            {
                currentGesture = new Gesture();
                HandleOnTouch(this, currentGesture);
                OnTouch(this, currentGesture);
            }

            if (firstTouch || hold)
            {
                GestureFrame gestureFrame = new GestureFrame(Input.mousePosition, hitInfo);
                currentGesture.AddGestureFrame(gestureFrame);
                HandleOnHold(this, currentGesture);
                OnHold(this, currentGesture);
            }


            if (drag)
            {
                HandleOnDrag(this, currentGesture);
                OnDrag(this, currentGesture);
                bool collisionLastFrame = currentGesture.LastFrame.Value.HitForCollider(_boxCollider).HasValue;
                bool collisionThisFrame = currentGesture.CurrentFrame.HitForCollider(_boxCollider).HasValue;

                if (collisionLastFrame && !collisionThisFrame)
                {
                    HandleOnDragLeave(this, currentGesture);
                    OnDragLeave(this, currentGesture);
                    currentGesture = null;
                }
            }

            if (release)
            {
                HandleOnRelease(this, currentGesture);
                OnRelease(this, currentGesture);
                currentGesture = null;
            }    
        }

        RaycastHit? GetHitInfo(Vector3 mousePosition)
        {
            Vector3 origin = Camera.main.ScreenToWorldPoint(mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(origin, Vector3.forward, 100f);
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                if (hit.collider == _boxCollider)
                {
                    return hit;
                }
            }
            return null;
        }
    }
}
