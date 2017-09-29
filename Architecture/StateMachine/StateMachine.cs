using System.Collections;

namespace Utilities
{
	public sealed class StateMachine
	{
		private IState _state;
		
		public StateMachine(IState startState)
		{
			_state = startState;
		}
		
		public IEnumerator Run()
		{
			while (_state != null)
			{
				var runRoutine = _state.Run();
				Diag.Crumb(this, "Running state " + _state);

				while (runRoutine.MoveNext())
					yield return null;
				
				Diag.Crumb(this, "Setting next state: " + _state.NextState);
				_state = _state.NextState;
			}
		}
	}
}
