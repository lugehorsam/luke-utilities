namespace Utilities
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	
	public class ViewState : IState
	{
		private readonly Func<IEnumerator> _enter;
		private readonly Func<IEnumerator> _exit;
		private readonly List<View> _views = new List<View>();
		
		public ViewState(Func<IEnumerator> enter = null, Func<IEnumerator> exit = null)
		{
			_enter = enter;
			_exit = exit;
		}

		public IEnumerator Exit()
		{
			Diag.Crumb(this, "Exiting state.");
			
			foreach (var view in _views)
				view.Destroy();
			
			yield return _exit?.Invoke();
		}

		public IEnumerator Enter()
		{
			Diag.Crumb(this, "Entering state.");
			yield return _enter?.Invoke();
		}

		protected void RegisterView(View view)
		{
			_views.Add(view);
		}
	}	
}
