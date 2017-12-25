namespace Utilities.Command
{	
	using System.Collections;

	public class CommandBase : IEnumerator
	{
		protected readonly Command _queue = new Command();

		public object Current => _queue.Current;

		protected CommandBase() {}

		public bool MoveNext()
		{
			return _queue.MoveNext();
		}

		public void Reset()
		{
			_queue.Reset();
		}
	}
}
