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
        
        private List<Transform> _Children => transform.GetChildren();
        
        //TODO Optimize rec calls with dynamic programming
        private Vector2 GetIdealLocalPosition(Transform child) {
            
            Vector2 newPosition = Vector2.zero;
            
            int currentChildIndex = _Children.IndexOf(child);
    
            if (currentChildIndex <= 0) {
                return newPosition;
            }
    
            Transform previousBehavior = _Children[currentChildIndex - 1];
    
            Rect childRect =  previousBehavior.GetComponent<RectTransform>().rect;

            //recursive
            Vector2 previousBehaviorPosition = GetIdealLocalPosition (previousBehavior);
            newPosition = previousBehaviorPosition;
            newPosition += new Vector2(childRect.width, childRect.height); //spacing
            newPosition += GetPaddingVector ();
            newPosition = ClampByFlow (previousBehaviorPosition, newPosition);
            if (ShouldWrap(child, newPosition)) {
                newPosition = GetLocalPositionFromWrap (newPosition);
            }
            return newPosition;
        }
    
        Vector2 GetLocalPositionFromWrap(Vector2 newPosition) {
            //TODO implement for vertical
            switch (FlowPolicy) {
            case FlexibleFlowPolicy.Horizontal:
            default:            
                return new Vector2 (0, newPosition.y - YSpacing);
            }
        }
    
        bool ShouldWrap(Transform newItem, Vector2 newPosition) {
            //TODO implement for vertical
            return Wrap && newPosition.x > WrapThreshold;
        }
    
        Vector2 GetPaddingVector() {
            return new Vector2 (XSpacing, YSpacing);
        }
    
        Vector2 ClampByFlow(Vector2 lastItemPos, Vector2 newGameObjPos) {
            switch (FlowPolicy) {
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
                child.localPosition = GetIdealLocalPosition(child);
            }           
        }

        public void SetLayoutVertical()
        {
            foreach (var child in _Children)
            {
                child.localPosition = GetIdealLocalPosition(child);
            }
        }
    }   
}
