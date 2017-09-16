namespace Utilities
{
	using System.Collections;
	using System.Collections.Generic;
	
	public class ViewState : IState
	{
		protected readonly EnumeratorQueue _enter = new EnumeratorQueue();
		protected readonly EnumeratorQueue _exit = new EnumeratorQueue();
		
		private readonly List<View> _views = new List<View>();
		
		public IEnumerator Exit()
		{
			Diag.Crumb(this, "Exiting state.");
			
			foreach (var view in _views)
				view.Destroy();

			_exit.Reset();
			return _exit;
		}

		public IEnumerator Enter()
		{
			Diag.Crumb(this, "Entering state.");
			
			_enter.Reset();
			return _enter;
		}

		protected void RegisterView(View view)
		{
			_views.Add(view);
		}
	}	
}
