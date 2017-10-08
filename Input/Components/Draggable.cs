namespace Utilities.Input
{
    using UnityEngine;

    public class Draggable : Selectable 
    {       
        Vector3 offsetFromMouse;

        protected override void OnDrag(TouchEventInfo info)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(info.WorldPosition);
            Vector3 newPosition = worldPoint - offsetFromMouse;
            transform.position = newPosition;
        }

        protected override void OnDeselect(TouchEventInfo info)
        {
            offsetFromMouse = Vector3.zero;
        }

        protected override void OnSelect(TouchEventInfo info)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(info.WorldPosition);
            offsetFromMouse = worldPoint - transform.position;
        }
    }
}

