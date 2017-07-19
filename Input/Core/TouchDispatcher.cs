using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.Input
{
    public sealed class TouchDispatcher
    {               
        public BoxCollider MeshCollider
        {
            get { return _meshCollider; }
        }
        
        private BoxCollider _meshCollider;
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
            _meshCollider = view.GameObject.AddComponent<BoxCollider>();
            _meshCollider.size = colliderSize;
            
            _rigidbody = view.GameObject.AddComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
            _view = view;
            dispatcher.OnLateUpdate += LateUpdate;
        }

        void LateUpdate()
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(worldPosition, Vector3.forward, 1000f);
            Diagnostics.Log("hits " + hits.ToFormattedString());

            bool isFirstDown = UnityEngine.Input.GetMouseButtonDown(0);
            bool isDown = UnityEngine.Input.GetMouseButton(0);
            bool isOver = hits.Any(hit => hit.collider == _meshCollider);
            bool isRelease = UnityEngine.Input.GetMouseButtonUp(0);

            _touchLogic.UpdateFrame(worldPosition, isDown, !isFirstDown, isRelease, isOver);

            TouchEventInfo info = new TouchEventInfo(this, _touchLogic, hits);

            if (_touchLogic.IsFirstDown)
            {
                Diagnostics.Log("first down");
                OnFirstDown(info);
            }

            if (_touchLogic.IsDrag)
            {
                Diagnostics.Log("on drag");
                OnDrag(info);
            }

            if (_touchLogic.IsRelease)
            {
                Diagnostics.Log("release");
                OnRelease(info);
            }
        }
    }
}
