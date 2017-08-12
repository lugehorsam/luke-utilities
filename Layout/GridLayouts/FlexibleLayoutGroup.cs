using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Utilities
{
    public sealed class FlexibleLayoutGroup : UIBehaviour, ILayoutGroup
    {
        public bool Wrap { get; set; }        
        public float WrapThreshold { get; set; }
        public FlexibleSpacingPolicy SpacingPolicy { get; set; }
        public FlexibleFlowPolicy FlowPolicy { get; set; }
        public float XSpacing { get; set; }
        public float YSpacing { get; set; }
        
        private List<RectTransform> _Children => RectTransform.GetChildren();        
        private RectTransform RectTransform => gameObject.GetComponent<RectTransform>();
        
        //TODO Optimize rec calls with dynamic programming
        private Vector2 GetIdealAnchoredPosition(RectTransform child) 
        {    
            Vector2 newAchoredPosition = Vector2.zero;
            
            int currentChildIndex = _Children.IndexOf(child);
    
            if (currentChildIndex <= 0) 
            {
                return newAchoredPosition;
            }
    
            RectTransform previousChild = _Children[currentChildIndex - 1];
            Rect previousChildRect =  previousChild.rect;

            //recursive
            Vector2 previousChildAnchoredPosition = GetIdealAnchoredPosition (previousChild);
            newAchoredPosition = previousChildAnchoredPosition;
            newAchoredPosition += new Vector2(previousChildRect.width, previousChildRect.height); //spacing
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
            switch (FlowPolicy) 
                {
                case FlexibleFlowPolicy.Horizontal:
                default:            
                    return new Vector2 (0, newPosition.y - YSpacing);
            }
        }
    
        bool ShouldWrap(Transform newItem, Vector2 newAnchoredPosition) 
        {
            //TODO implement for vertical
            return Wrap && newAnchoredPosition.x > WrapThreshold;
        }
    
        Vector2 GetPaddingVector() 
        {
            return new Vector2 (XSpacing, YSpacing);
        }
    
        Vector2 ClampByFlow(Vector2 lastItemPos, Vector2 newGameObjPos) 
        {
            switch (FlowPolicy) 
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
            foreach (var child in _Children)
            {
                child.anchoredPosition = GetIdealAnchoredPosition(child);
            }           
        }

        public void SetLayoutVertical()
        {
            foreach (var child in _Children)
            {
                child.anchoredPosition = GetIdealAnchoredPosition(child);
            }
        }
    }   
}
