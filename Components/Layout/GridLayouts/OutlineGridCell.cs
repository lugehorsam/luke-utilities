using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{ 
	public class OutlineGridCell : View
	{
		private readonly IGridLayout _gridLayout;
		
		public OutlineGridCell(IGridLayout gridLayout)
		{
			_gridLayout = gridLayout;
		}		    
	}
}
