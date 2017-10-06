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
			Diag.Log("calling raycast");
			var graphics = GraphicRegistry.GetGraphicsForCanvas(GetComponent<Canvas>());
			for (int i = 0; i < graphics.Count; i++)
			{
				var graphic = graphics[i];
				RaycastResult raycastResult = new RaycastResult()
				{
					gameObject = graphic.gameObject,
					module = this,
					distance = 0,
					screenPosition = (Vector2) eventData.position,
					index = (float) resultAppendList.Count,
					depth =1,
					sortingLayer = GetComponent<Canvas>().sortingLayerID,
					sortingOrder = GetComponent<Canvas>().sortingOrder
				};
				resultAppendList.Add(raycastResult);
			}


		}

		private void OnGUI()
		{			
		}

		public override bool IsActive()
		{Diag.Log("is active called " + base.IsActive());
			return base.IsActive();
		}
	}
	

}