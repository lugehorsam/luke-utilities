namespace Utilities
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	
	public class ControllerState : Controller, IState
	{
		protected readonly EnumeratorQueue _enter = new EnumeratorQueue();
		protected readonly EnumeratorQueue _exit = new EnumeratorQueue();
		private readonly List<Controller> _controllers = new List<Controller>();

		public Transform Root { get; set; }
		
		public ControllerState()
		{
			_enter.Id = $"Enter queue: {GetType()}";
			_exit.Id = $"Exit queue: {GetType()}";			
		}

		public IEnumerator Exit()
		{
			Diag.Crumb(this, "Exiting state.");
			_exit.Reset();
			yield return _exit;
			RemoveFromScene();
		}

		public IEnumerator Enter()
		{
			Diag.Crumb(this, "Entering state.");	
			_enter.Reset();
			AddToScene(Root);
			return _enter;
		}

		protected void RegisterController(Controller controller)
		{
			controller.Transform.SetParent(Transform, false);
			_controllers.Add(controller);
		}
	}	
}
