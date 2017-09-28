namespace Utilities
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	
	public class StateController : Controller, IState
	{
		protected readonly EnumeratorQueue _enter = new EnumeratorQueue();
		protected readonly EnumeratorQueue _exit = new EnumeratorQueue();

		public Transform Root { get; set; }
		
		public StateController()
		{
			_enter.Id = $"Enter queue: {GetType()}";
			_exit.Id = $"Exit queue: {GetType()}";			
		}

		public IEnumerator Exit()
		{
			Diag.Crumb(this, "Exiting state.");
			_exit.Reset();
			return _exit;
		}

		public IEnumerator Enter()
		{   
			Diag.Log("returned enter");
			_enter.Reset();
			Instantiate(Root);
			return _enter;
		}

		protected void RegisterController(Controller controller)
		{
			controller.Instantiate(Transform);
			controller.Transform.SetParent(Transform, false);
		}
	}	
}
