using System.ComponentModel;

namespace Utilities.Input
{
    using UnityEngine;

    public class TouchDispatcher : MonoBehaviour
    {
        [SerializeField] private Dimension _dimension;
        
        public ITouchState TouchState => _touchLogic;
       
        private readonly TouchLogic _touchLogic = new TouchLogic();
        
        private void Update()
        {
            if (!ValidateDimension())
                return;
            
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            UpdateTouchLogic(mouseWorldPoint);
            
            Diag.Log("after update touch logic it is " + _touchLogic.IsFirstDown);
            TouchEventInfo touchInfo = CreateTouchInfo(mouseWorldPoint);
            DispatchEvents(touchInfo);                                             
            OnProcess(touchInfo);
        }        
        
        protected virtual void OnFirstDown(TouchEventInfo info) {}
        protected virtual void OnRelease(TouchEventInfo info){}
        protected virtual void OnDrag(TouchEventInfo info) {}
        protected virtual void OnDownOff(TouchEventInfo info) {}
        protected virtual void OnProcess(TouchEventInfo info) {}

        private bool DetermineIsMouseOver(Vector3 mouseWorldPoint)
        {
            switch (_dimension)
            {
                case Dimension.Two:
                    return mouseWorldPoint.IsOver(GetComponent<Collider2D>());
                case Dimension.Three:
                    return mouseWorldPoint.IsOver(GetComponent<Collider>());
            }

            return false;
        }

        private bool ValidateDimension()
        {
            if (_dimension == Dimension.None)
                throw new InvalidEnumArgumentException("Must have a dimension.");

            return true;
        }

        private void UpdateTouchLogic(Vector3 mouseWorldPoint)
        {            
            bool isFirstDown = Input.GetMouseButtonDown(0);
            bool isDown = Input.GetMouseButton(0);
            bool isOver = DetermineIsMouseOver(mouseWorldPoint);            
            bool isRelease = Input.GetMouseButtonUp(0);
            
            mouseWorldPoint.z = transform.position.z;
            
            _touchLogic.UpdateFrame(mouseWorldPoint, isDown, !isFirstDown, isRelease, isOver);
        }

        private void DispatchEvents(TouchEventInfo touchInfo)
        {                              
            Diag.Log(_touchLogic.IsFirstDown.ToString());
            
            if (_touchLogic.IsFirstDown)
            {
                OnFirstDown(touchInfo);
            }

            if (_touchLogic.IsFirstDownOff)
            {
                OnDownOff(touchInfo);
            }

            if (_touchLogic.IsDrag)
            {
                OnDrag(touchInfo);
            }

            if (_touchLogic.IsRelease)
            {
                OnRelease(touchInfo);
            }
        }

        private TouchEventInfo CreateTouchInfo(Vector3 mouseWorldPoint)
        {
            TouchEventInfo info;

            if (_dimension == Dimension.Two)
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(mouseWorldPoint, Vector2.zero, 1000f);
                info = new TouchEventInfo(this, _touchLogic, hits, mouseWorldPoint);
            }
            else
            {
                RaycastHit[] hits = Physics.RaycastAll(mouseWorldPoint, Vector3.forward, 1000f);
                info = new TouchEventInfo(this, _touchLogic, hits, mouseWorldPoint);
            }                
            
            return info;
        }
    }    
}
