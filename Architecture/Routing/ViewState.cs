namespace Utilities
{
	using System.Collections;
	using System.Collections.Generic;
	
	public class ViewState : IState
	{
		private readonly IEnumerator _enter;
		private readonly IEnumerator _exit;
		private readonly List<View> _views = new List<View>();
		
		public ViewState(IEnumerator enter = null, IEnumerator exit = null)
		{
			_enter = enter;
			_exit = exit;
		}

		public IEnumerator Exit()
		{
			yield return _exit;
		}

		public IEnumerator Enter()
		{
			yield return _enter;
		}		

		protected void RegisterView(View view) {}
	}	
}
