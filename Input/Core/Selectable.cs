
namespace Utilities.Input
{
	public delegate void SelectableHandler(Selectable selectable);
	
	public class Selectable
	{
		public SelectableHandler OnSelect = delegate { };
		public SelectableHandler OnDeselect = delegate { };

		private bool _selected;
		
		private readonly TouchDispatcher<Selectable> _touchDispatcher;

		public Selectable(TouchDispatcher<Selectable> touchDispatcher)
		{
			_touchDispatcher = touchDispatcher;
			_touchDispatcher.OnEndFrame += HandleEndFrame;
		}

		void HandleEndFrame(TouchLogic touchLogic)
		{
			if ((touchLogic.IsFirstDownOff || touchLogic.IsFirstDownOn) && _selected)
			{
				_selected = false;
				OnDeselect(this);
			}
			
			if (touchLogic.IsFirstDownOn && _selected)
			{
				_selected = true;
				OnSelect(this);
			}
		}
	}	
}