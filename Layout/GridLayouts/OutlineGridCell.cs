namespace Utilities
{ 
	public class OutlineGridCell : Controller
	{
		private readonly IGridLayout _gridLayout;
		
		public OutlineGridCell(IGridLayout gridLayout)
		{
			_gridLayout = gridLayout;
		}		    
	}
}
