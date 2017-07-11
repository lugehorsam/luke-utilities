using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.Input
{
    public sealed class TouchDispatcher
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
        
        public View View
        {
            get { return _view; }
        }
        
        private View _view;

        public TouchDispatcher(LifecycleDispatcher dispatcher, View view, Vector3 colliderSize)
        {            
            _boxCollider = view.GameObject.AddComponent<BoxCollider>();
            _boxCollider.size = colliderSize;
            
            _rigidbody = view.GameObject.AddComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
            _view = view;
            dispatcher.OnLateUpdate += LateUpdate;
        }
        
        void LateUpdate()
        {           
            bool mouseIsDown = UnityEngine.Input.GetMouseButton(0);
            bool mouseWasDownOverThis = currentGesture != null;            
            
            RaycastHit? hitInfo = GetHitInfo(UnityEngine.Input.mousePosition);

            bool firstTouch = UnityEngine.Input.GetMouseButtonDown(0) && hitInfo.HasValue;
            bool hold = mouseWasDownOverThis && mouseIsDown;
            bool release = mouseWasDownOverThis && !mouseIsDown;
            bool drag = hold && currentGesture.MousePositionCurrent != currentGesture.MousePositionLast;
            
            if (firstTouch)
            {
                currentGesture = new Gesture();
                OnTouch(this, currentGesture);
            }

            if (firstTouch || hold)
            {
                GestureFrame gestureFrame = new GestureFrame(UnityEngine.Input.mousePosition, hitInfo);
                currentGesture.AddGestureFrame(gestureFrame);
                OnHold(this, currentGesture);
            }

            if (drag)
            {
                OnDrag(this, currentGesture);
                bool collisionLastFrame = currentGesture.LastFrame.Value.HitForCollider(_boxCollider).HasValue;
                bool collisionThisFrame = currentGesture.CurrentFrame.HitForCollider(_boxCollider).HasValue;

                if (collisionLastFrame && !collisionThisFrame)
                {
                    OnDragLeave(this, currentGesture);
                }
            }

            if (release)
            {
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
