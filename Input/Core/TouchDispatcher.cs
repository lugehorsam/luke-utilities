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

        public event Action<TouchEventInfo> OnFirstDown = delegate { };
        public event Action<TouchEventInfo> OnRelease = delegate { };
        public event Action<TouchEventInfo> OnDrag = delegate { };
        
        public View View
        {
            get { return _view; }
        }
        
        private View _view;

        private TouchLogic _touchLogic = new TouchLogic();

        public TouchDispatcher(UnityLifecycleDispatcher dispatcher, View view, Vector3 colliderSize)
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
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);            
            RaycastHit[] hits = Physics.RaycastAll(mouseWorldPosition, Vector3.forward, 100f);

            UpdateTouchLogic(mouseWorldPosition, hits);
            DispatchTouchEvents(hits);                       
        }

        void DispatchTouchEvents(RaycastHit[] hits)
        {
            TouchEventInfo info = new TouchEventInfo(this, _touchLogic, hits);
            
            if (_touchLogic.IsFirstDown)
            {
                OnFirstDown(info);
            }
            
            if (_touchLogic.IsDrag)
            {
                OnDrag(info);
            }
            
            if (_touchLogic.IsRelease)
            {
                OnRelease(info);
            }
        }

        void UpdateTouchLogic(Vector3 mouseWorldPosition, RaycastHit[] hits)
        {
           
            bool mouseIsDown = UnityEngine.Input.GetMouseButton(0);
            bool mouseOver = hits.Any(hit => hit.collider == _boxCollider);      
            
            _touchLogic.UpdateFrame(mouseWorldPosition, mouseIsDown, mouseOver);                                              
        }
    }
}
