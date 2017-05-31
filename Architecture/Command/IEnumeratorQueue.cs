using System;
using System.Collections;

namespace Utilities
{
	public interface IEnumeratorQueue
	{
		void AddSerial(IEnumerator enumerator);
		void AddParallel(Action action);
		void AddParallel(IEnumerator enumerator);
	}	
}
