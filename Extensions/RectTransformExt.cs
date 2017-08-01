using UnityEngine;

namespace Utilities
{
	public static class RectTransformExt {

		public static bool Overlaps(this RectTransform rectTrans1, RectTransform rectTrans2, Canvas canvas)
		{
			Rect rect1 = rectTrans1.GetScreenRect(canvas);
			Rect rect2 = rectTrans2.GetScreenRect(canvas);

			return rect1.Overlaps(rect2);
		}
		
		public static void ResetLocalValues( this RectTransform t ) {
			t.anchoredPosition = Vector2.zero;
			t.localRotation = Quaternion.identity;
			t.localScale = Vector3.one;
			t.sizeDelta = Vector2.zero;
		}
    
		/// In world space
		public static Vector3 CenterWorldSpace( this RectTransform t )
		{
			var pos = VectorExt.Add(t.position, new Vector3(t.rect.width/2, -t.rect.height/2, 0));          
			return pos;
		}
		
		/// <summary>
		/// Given a rect transform and a canvas, returns a rect representing the bounds of the rect transform in screen space.
		/// </summary>
		/// <returns>The screen rect.</returns>
		/// <param name="rectTransform">Rect transform.</param>
		/// <param name="canvas">Canvas.</param>
		public static Rect GetScreenRect (this RectTransform rectTransform, Canvas canvas)
		{

			Vector3 [] corners = new Vector3 [4];
			Vector3 [] screenCorners = new Vector3 [2];

			rectTransform.GetWorldCorners (corners);

			if (canvas.renderMode == RenderMode.ScreenSpaceCamera || canvas.renderMode == RenderMode.WorldSpace) 
			{
				screenCorners [0] = RectTransformUtility.WorldToScreenPoint (canvas.worldCamera, corners [1]);
				screenCorners [1] = RectTransformUtility.WorldToScreenPoint (canvas.worldCamera, corners [3]);
			} else 
			{
				screenCorners [0] = RectTransformUtility.WorldToScreenPoint (null, corners [1]);
				screenCorners [1] = RectTransformUtility.WorldToScreenPoint (null, corners [3]);
			}

			screenCorners [0].y = Screen.height - screenCorners [0].y;
			screenCorners [1].y = Screen.height - screenCorners [1].y;

			return new Rect (screenCorners [0], screenCorners [1] - screenCorners [0]);
		}
	}
}
