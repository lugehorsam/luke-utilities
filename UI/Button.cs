using UnityEngine.EventSystems;

namespace Utilities
{
	public class Button : UnityEngine.UI.Button 
	{
		public override void OnPointerDown(PointerEventData eventData)
		{Diag.Log("on pointer down called");
			base.OnPointerDown(eventData);
		}

		public override void OnPointerEnter(PointerEventData eventData)
		{
			Diag.Log("pointer enter called");
			base.OnPointerEnter(eventData);
		}
	}
}