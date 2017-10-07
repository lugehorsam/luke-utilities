using UnityEngine.UI;

namespace Utilities.UI
{
	using UnityEngine.EventSystems;

	public class Draggable : UIBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
	{
		private LayoutElement _layoutElement;
		private LayoutElement _LayoutElement => _layoutElement ?? (_layoutElement = GetComponent<LayoutElement>());
		
		public void OnDrag(PointerEventData eventData)
		{
			transform.position = eventData.position;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (_LayoutElement != null)
				_LayoutElement.ignoreLayout = true;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (_LayoutElement != null)
				_LayoutElement.ignoreLayout = true;
		}
	}	
}
