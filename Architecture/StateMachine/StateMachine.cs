namespace Utilities
{
	public sealed class StateMachine
	{
		private IState _state;
		
		public IState State => _state;
		
		public StateTransition CreateTransition(IState newState)
		{
			return StateTransition.FromMachine(this, newState);
		}
		
		public class StateTransition : Command
		{
			private StateTransition(IState oldState, IState newState, StateMachine machine)
			{
				if (oldState != null)
					_queue.AddSerial(oldState.Exit());

				machine._state = newState;
			
				if (newState != null)
					_queue.AddSerial(newState.Enter());
			}

			public static StateTransition FromMachine(StateMachine machine, IState newState)
			{
				return new StateTransition(machine._state, newState, machine);
			}
		}
	}
}
