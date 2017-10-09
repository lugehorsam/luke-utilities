namespace Utilities.Input
{
    using UnityEngine;

    public class Draggable : MonoBehaviour 
    {       
        Vector3 offsetFromMouse;

        private void OnMouseDrag()
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = worldPoint - offsetFromMouse;
            transform.position = newPosition;
        }

        private void OnMouseUp()
        {
            
        }

        private void OnMouseDown()
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offsetFromMouse = worldPoint - transform.position;
        }
    }
}
