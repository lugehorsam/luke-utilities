namespace Utilities.Input
{		
	public class Selectable : TouchDispatcher 
	{
		private bool _currentlySelected;

		protected override void OnProcess(TouchEventInfo info)
		{
			bool wasSelected = _currentlySelected;
			
			if (_touchLogic.IsFirstDownOff && wasSelected)
			{
				_currentlySelected = false;
				OnDeselect(info);
			}
			
			if (_touchLogic.IsFirstDownOn && !wasSelected)
			{
				_currentlySelected = true;
				OnSelect(info);
			}
		}

		protected virtual void OnSelect(TouchEventInfo info)
		{
		}

		protected virtual void OnDeselect(TouchEventInfo info)
		{
			
		}
	}	
}