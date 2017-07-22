﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.Input
{
    public class TouchDispatcher
    {               
        public Collider Collider { get; }

        private readonly Rigidbody _rigidbody;

        public event Action<TouchLogic> OnEndFrame = delegate { };
        public event Action<TouchEventInfo> OnFirstDown = delegate { };
        public event Action<TouchEventInfo> OnRelease = delegate { };
        public event Action<TouchEventInfo> OnDrag = delegate { };
        public event Action<TouchEventInfo> OnDownOff = delegate { };
       
        private TouchLogic _touchLogic = new TouchLogic();

        public TouchDispatcher(UnityLifecycleDispatcher dispatcher, Collider collider)
        {
            Collider = collider;
            _rigidbody = collider.GetView().GameObject.AddComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
            dispatcher.OnLateUpdate += LateUpdate;
        }

        void LateUpdate()
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(worldPosition, Vector3.forward, 1000f);

            bool isFirstDown = UnityEngine.Input.GetMouseButtonDown(0);
            bool isDown = UnityEngine.Input.GetMouseButton(0);
            bool isOver = hits.Any(hit => hit.collider == Collider);
            bool isRelease = UnityEngine.Input.GetMouseButtonUp(0);

            _touchLogic.UpdateFrame(worldPosition, isDown, !isFirstDown, isRelease, isOver);

            TouchEventInfo info = new TouchEventInfo(this, _touchLogic, hits);

            if (_touchLogic.IsFirstDownOn)
            {
                OnFirstDown(info);
            }

            if (_touchLogic.IsFirstDownOff)
            {
                OnDownOff(info);
            }

            if (_touchLogic.IsDrag)
            {
                OnDrag(info);
            }

            if (_touchLogic.IsRelease)
            {
                OnRelease(info);
            }
            
            OnEndFrame(_touchLogic);
        }
    }
}