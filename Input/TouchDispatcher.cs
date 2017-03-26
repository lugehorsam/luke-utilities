namespace Utilities.Input
{
    using UnityEngine;
    using System;

    public class TouchDispatcher<T> : MonoBehaviour
    {
        public T DispatchObject
        {
            get;
            private set;
        }
        
        private new Collider collider;
        private new Rigidbody rigidbody;

        public event Action<TouchDispatcher<T>, Gesture> OnTouch = (arg1, arg2) => { };
        public event Action<TouchDispatcher<T>, Gesture> OnHold = (arg1, arg2) => { };
        public event Action<TouchDispatcher<T>, Gesture> OnRelease = (arg1, arg2) => { };
        public event Action<TouchDispatcher<T>, Gesture> OnDrag = (arg1, arg2) => { };
        public event Action<TouchDispatcher<T>, Gesture> OnDragLeave = (arg1, arg2) => { };

        protected virtual void HandleOnTouch(TouchDispatcher<T> touchDispatcher, Gesture gestureFrame) { }
        protected virtual void HandleOnHold(TouchDispatcher<T> touchDispatcher, Gesture gestureFrame) { }
        protected virtual void HandleOnRelease(TouchDispatcher<T> touchDispatcher, Gesture gestureFrame) { }
        protected virtual void HandleOnDrag(TouchDispatcher<T> touchDispatcher, Gesture gestureFrame) { }
        protected virtual void HandleOnDragLeave(TouchDispatcher<T> touchDispatcher, Gesture gestureFrame) { }

        protected Gesture currentGesture;

        public void Init(
            Vector3 colliderSize,
            T dispatchObject
        )
        {            
            var boxCollider =  gameObject.AddComponent<BoxCollider>();
            boxCollider.size = colliderSize;
            collider = boxCollider;
            rigidbody = gameObject.AddComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
            DispatchObject = dispatchObject;

        }
        
        void Update()
        {
            if (collider == null)
            {
                Diagnostics.LogWarning("Touch dispatcher added to " + name + " with no collider");
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
                bool collisionLastFrame = currentGesture.LastFrame.Value.HitForCollider(collider).HasValue;
                bool collisionThisFrame = currentGesture.CurrentFrame.HitForCollider(collider).HasValue;

                if (collisionLastFrame && !collisionThisFrame)
                {
                    HandleOnDragLeave(this, currentGesture);
                    OnDragLeave(this, currentGesture);
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
                if (hit.collider == collider)
                {
                    return hit;
                }
            }
            return null;
        }
    }
}
