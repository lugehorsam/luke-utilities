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
			bool moveNext = _queue.MoveNext();

			if (!moveNext)
			{
				Diag.Crumb(this, "Command complete");
			}
			
			return moveNext;
		}

		public void Reset()
		{
			_queue.Reset();
		}
	}
}
