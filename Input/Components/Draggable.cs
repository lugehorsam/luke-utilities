namespace Utilities.Input
{
    using System;

    using UnityEngine;

    public class Draggable : MonoBehaviour 
    {       
        Vector3 offsetFromMouse;
        
        public event Action<Draggable> OnDrag = delegate
        {
            
        };

        private void OnMouseDrag()
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = worldPoint - offsetFromMouse;
            transform.position = newPosition;
            OnDrag(this);
        }

        private void OnMouseDown()
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offsetFromMouse = worldPoint - transform.position;
        }
    }
}
