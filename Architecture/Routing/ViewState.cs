namespace Utilities
{
	using System;
	using System.Collections.Generic;
	
	public class ViewState : IState
	{
		private readonly Action _onEnter;
		private readonly Action _onExit;
		private readonly List<View> _views = new List<View>();
		
		public ViewState(Action onEnter, Action onExit)
		{
			_onEnter = onEnter;
			_onExit = onExit;
		}

		public void OnExit()
		{
			_onExit?.Invoke();
		}

		public void OnEnter()
		{
			_onEnter?.Invoke();
		}		

		protected void RegisterView(View view) {}
	}	
}
