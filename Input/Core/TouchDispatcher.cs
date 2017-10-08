namespace Utilities.Input
{
    using UnityEngine;

    public class TouchDispatcher : MonoBehaviour
    {                       

        public ITouchState TouchState => _touchLogic;
       
        protected readonly TouchLogic _touchLogic = new TouchLogic();
        
        private void Update()
        {            
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(mouseWorldPoint, Vector3.forward, 1000f);

            bool isFirstDown = Input.GetMouseButtonDown(0);
            bool isDown = Input.GetMouseButton(0);
            bool isOver = mouseWorldPoint.IsOver(GetComponent<Collider>());
            bool isRelease = Input.GetMouseButtonUp(0);

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
            
            OnProcess(info);
        }        
        
        protected virtual void OnFirstDown(TouchEventInfo info) {}
        protected virtual void OnRelease(TouchEventInfo info){}
        protected virtual void OnDrag(TouchEventInfo info) {}
        protected virtual void OnDownOff(TouchEventInfo info) {}
        protected virtual void OnProcess(TouchEventInfo info) {}
    }    
}
