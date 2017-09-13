using System.Collections;

namespace Utilities
{
	public class StateMachine<T> where T : IState
	{
		public T State => _state.Value;
		private readonly Reactive<T> _state = new Reactive<T>();

		private readonly EnumeratorQueue _queue;

		public StateMachine()
		{
			_state.OnPropertyChanged += HandleStateChanged;
		}
		
		public IEnumerator SetState(T newState)
		{
			_state.Value = newState;
			return _queue;
		}

		private void HandleStateChanged(T oldState, T newState)
		{
			_queue.AddSerial(oldState?.Exit());
			_queue.AddSerial(newState?.Enter());
		}
	}
}
