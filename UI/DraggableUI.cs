namespace Utilities.UI
{
	using UnityEngine;
	using UnityEngine.UI;
	using UnityEngine.EventSystems;

	public class DraggableUI : UIBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
	{
		private LayoutElement _layoutElement;
		private RectTransform _rectTransform;
		private Canvas _canvas;
		private RectTransform _canvasTransform;
		private Transform _lastParent;		
		private Vector3? _lastPosition;

		private LayoutElement _LayoutElement => _layoutElement ?? (_layoutElement = GetComponent<LayoutElement>());		
		private RectTransform _RectTransform => _rectTransform ?? (_rectTransform = GetComponent<RectTransform>());
		private Canvas _Canvas => _canvas ?? (_canvas = GetComponentInParent<Canvas>());
		private RectTransform _CanvasTransform => _canvasTransform ?? (_canvasTransform = _Canvas.GetComponent<RectTransform>());

		protected override void OnBeforeTransformParentChanged()
		{
			_lastParent = transform.parent;
			_lastPosition = transform.position;
		}

		public void OnDrag(PointerEventData eventData)
		{
			var pointerPos = eventData.position;

			pointerPos.y = eventData.position.y - _CanvasTransform.rect.height;
			_RectTransform.anchoredPosition = pointerPos;
		}

		public void OnPointerDown(PointerEventData eventData)
		{	
			if (_LayoutElement != null)
				_LayoutElement.ignoreLayout = true;

			transform.SetParent(_Canvas.transform, true);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (_LayoutElement != null)
				_LayoutElement.ignoreLayout = false;
			
			transform.SetParent(_lastParent, false);
		}		
	}	
}
