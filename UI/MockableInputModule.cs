namespace Utilities
{
	using UnityEngine;
	using UnityEngine.EventSystems;

	[ExecuteInEditMode]
	public class MockableInputModule : StandaloneInputModule 
	{
		private void OnGUI()
		{
			m_InputOverride = m_InputOverride ?? 
			                  GetComponent<MockableInput>() ?? 
			                  gameObject.AddComponent<MockableInput>();
			Process();
		}

		protected override void ProcessMove(PointerEventData pointerEvent)
		{
			Diag.Log("process move called with pointer event " + pointerEvent);
			Diag.Log("pointer " + pointerEvent.position);
			Diag.Log("raycast is " + pointerEvent.pointerCurrentRaycast);
			
			base.ProcessMove(pointerEvent);
		}
	}	
}
