namespace Utilities
{
	using UnityEngine.UI;
	
	public class ButtonColorsBinding : PropertyBinding<ColorBlock, Button>
	{
		public override ColorBlock GetProperty()
		{
			return _Component.colors;
		}

		public override void SetProperty(ColorBlock property)
		{
			_Component.colors = property;
		}
	}	
}
