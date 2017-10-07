using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

namespace Utilities
{
	[ExecuteInEditMode]
	public class MockableRaycaster : GraphicRaycaster 
	{
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			var canvas = GetComponent<Canvas>();
			var graphics = GraphicRegistry.GetGraphicsForCanvas(canvas);
			
			for (int i = 0; i < graphics.Count; i++)
			{
				var graphic = graphics[i];

				Rect graphicRect = graphic.rectTransform.GetScreenRect(canvas);

				GameObject graphicGameObject = null;
				
				if (graphicRect.Contains(eventData.pressPosition, true))
					graphicGameObject = graphic.gameObject;
				
				RaycastResult raycastResult = new RaycastResult()
				{
					gameObject = graphicGameObject,
					module = this,
					distance = 0,
					screenPosition = eventData.position,
					index = resultAppendList.Count,
					depth = 1,
					sortingLayer = GetComponent<Canvas>().sortingLayerID,
					sortingOrder = GetComponent<Canvas>().sortingOrder
				};
				resultAppendList.Add(raycastResult);
			}


		}
	}
}
