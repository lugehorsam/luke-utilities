using UnityEngine;

namespace Utilities
{
    public sealed class FlexibleLayoutGroup : MonoBehaviour
    {
        [SerializeField] private bool _wrap;
        [SerializeField] private float _wrapThreshold;
        [SerializeField] private FlexibleFlowPolicy _flowPolicy;
        [SerializeField] private float _xSpacing;
        [SerializeField] private float _ySpacing;
                
        private Vector2 GetIdealLocalPosition(Transform child) 
        {    
            Vector2 newAchoredPosition = Vector2.zero;

            int currentChildIndex = child.GetSiblingIndex();
    
            if (currentChildIndex <= 0) 
            {
                return newAchoredPosition;
            }
    
            Transform previousChild = transform.GetChild(currentChildIndex - 1);

            Vector2 previousChildSize = previousChild.GetComponent<FlexibleLayoutBehaviour>().Size;
            
            //recursive
            Vector2 previousChildAnchoredPosition = GetIdealLocalPosition (previousChild);
            newAchoredPosition = previousChildAnchoredPosition;
            newAchoredPosition += new Vector2(previousChildSize.x, previousChildSize.y); //spacing
            newAchoredPosition += GetPaddingVector ();
            newAchoredPosition = ClampByFlow (previousChildAnchoredPosition, newAchoredPosition);
            
            if (ShouldWrap(child, newAchoredPosition)) 
            {
                newAchoredPosition = GetLocalPositionFromWrap (newAchoredPosition);
            }
            
            return newAchoredPosition;
        }
    
        Vector2 GetLocalPositionFromWrap(Vector2 newPosition) 
        {
            //TODO implement for vertical
            switch (_flowPolicy) 
                {
                case FlexibleFlowPolicy.Horizontal:
                default:            
                    return new Vector2 (0, newPosition.y - _ySpacing);
            }
        }
    
        bool ShouldWrap(Transform newItem, Vector2 newAnchoredPosition) 
        {
            //TODO implement for vertical
            return _wrap && newAnchoredPosition.x > _wrapThreshold;
        }
    
        Vector2 GetPaddingVector() 
        {
            return new Vector2 (_xSpacing, _ySpacing);
        }
    
        Vector2 ClampByFlow(Vector2 lastItemPos, Vector2 newGameObjPos) 
        {
            switch (_flowPolicy) 
            {
                case FlexibleFlowPolicy.None:
                    return newGameObjPos;
                case FlexibleFlowPolicy.Horizontal:
                    return new Vector2 (newGameObjPos.x, lastItemPos.y);
                case FlexibleFlowPolicy.Vertical:
                    return new Vector2 (lastItemPos.x, newGameObjPos.y);
                default:
                    return Vector2.zero;
            }
        }

        public void SetLayoutHorizontal()
        {
            foreach (var child in transform.GetChildren())
            {
                child.localPosition = GetIdealLocalPosition(child);
            }           
        }

        public void SetLayoutVertical()
        {
            foreach (var child in transform.GetChildren())
            {
                child.localPosition = GetIdealLocalPosition(child);
            }
        }
    }   
}
