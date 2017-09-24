using UnityEngine;

namespace Utilities
{
	using System.Collections;
	using System.Collections.Generic;
	
	public abstract class ViewState : IState
	{
		protected readonly EnumeratorQueue _enter = new EnumeratorQueue();
		protected readonly EnumeratorQueue _exit = new EnumeratorQueue();
		private readonly List<Controller> controllers = new List<Controller>();
		private readonly Transform _root;

		protected ViewState(Transform root)
		{
			_enter.Id = $"Enter queue: {GetType()}";
			_exit.Id = $"Exit queue: {GetType()}";

			_root = root;
		}

		public IEnumerator Exit()
		{
			Diag.Crumb(this, "Exiting state.");
		
			_exit.Reset();
			yield return _exit;

			foreach (var view in controllers)
				view.Destroy();
		}

		public IEnumerator Enter()
		{
			Diag.Crumb(this, "Entering state.");	
			_enter.Reset();
			return _enter;
		}

		protected void RegisterController(Controller controller)
		{
			controller.Transform.SetParent(_root);
			controllers.Add(controller);
		}
	}	
}
