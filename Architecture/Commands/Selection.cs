namespace Utilities.Commands
{
	using System.Linq;
	using System.Collections;
	using System.Collections.Generic;
	
	public class Selection : Command
	{
		private readonly IEnumerable<ISelectable> _selectables;
		
		public ISelectable Choice { get; private set; }	
		
		public Selection(IEnumerable<ISelectable> selectables)
		{
			_selectables = selectables;
			_queue.AddSerial(WaitForSelection);
		}

		private IEnumerator WaitForSelection()
		{
			while (!_selectables.Any(_selectable => _selectable.IsSelected))
			{
				yield return null;
			}

			Choice = _selectables.First();
		}
	}	
}
