using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.Input
{
    public class TouchDispatcher : MonoBehaviour
    {                       
        private readonly Rigidbody _rigidbody;

        public event Action<TouchLogic> OnEndFrame = delegate { };
        public event Action<TouchEventInfo> OnFirstDown = delegate { };
        public event Action<TouchEventInfo> OnRelease = delegate { };
        public event Action<TouchEventInfo> OnDrag = delegate { };
        public event Action<TouchEventInfo> OnDownOff = delegate { };

        public ITouchState TouchState => _touchLogic;
       
        private TouchLogic _touchLogic = new TouchLogic();

        private readonly UnityLifecycleDispatcher _dispatcher;
        
        void Update()
        {            
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(mouseWorldPoint, Vector3.forward, 1000f);

            bool isFirstDown = UnityEngine.Input.GetMouseButtonDown(0);
            bool isDown = UnityEngine.Input.GetMouseButton(0);
            bool isOver = mouseWorldPoint.IsOver(null); //TODO replace
            bool isRelease = UnityEngine.Input.GetMouseButtonUp(0);

            mouseWorldPoint.z = transform.position.z;
            
            _touchLogic.UpdateFrame(mouseWorldPoint, isDown, !isFirstDown, isRelease, isOver);

            var info = new TouchEventInfo(this, _touchLogic, hits, mouseWorldPoint);

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
