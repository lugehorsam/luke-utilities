using System;
using System.Collections;
using System.Collections.Generic;

namespace Utilities
{
	public interface IEnumeratorQueue
	{
		void AddSerial(IEnumerator enumerator);
		void AddParallel(Action action);
		void AddRange(IEnumerable<IEnumerator> enumerators);
		void AddParallel(IEnumerator enumerator);
	}	
}
