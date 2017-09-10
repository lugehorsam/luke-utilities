namespace Utilities
{	
	using System.Collections;

	public class Command : IEnumerator
	{
		protected readonly EnumeratorQueue _queue = new EnumeratorQueue();

		public object Current => _queue.Current;

		protected Command() {}

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
