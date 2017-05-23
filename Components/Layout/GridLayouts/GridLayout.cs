using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{

    /// <summary>
    /// Arbitrary grid layout. Bottom left is row 0, col 0
    /// </summary>
    public class GridLayout<T> : Layout<T>, IGridLayout where T : IGridMember, ILayoutMember
    {
        public event Action<int> OnCellTouch = delegate { };
        protected override string Name { get { return "Grid Layout";  } }

        IGrid IGridLayout.Grid
        {
            get { return _grid; }
        }

        public Grid<T> Grid
        {
            get { return _grid; }
        }

        public float CellHeight
        {
            get { return _cellHeight; }
        }

        public float CellWidth
        {
            get { return _cellWidth; }
        }

        public float TotalHeight
        {
            get { return CellHeight * _grid.Rows; }
        }

        private float TotalWidth
        {
            get { return CellWidth * _grid.Columns; }
        }

        private readonly Grid<T> _grid;
        private readonly float _cellWidth;
        private readonly float _cellHeight;        
        private readonly RectTransform _rectTransform;

        public GridLayout(Grid<T> grid, float cellWidth, float cellHeight) : base(grid)
        {
            _grid = grid;
            _rectTransform = GameObject.AddComponent<RectTransform>();
            _cellWidth = cellWidth;
            _cellHeight = cellHeight;
            _grid.OnAfterItemAdd += delegate { DoLayout(); };
            _grid.OnAfterItemRemove += delegate { DoLayout(); };
        }
               
        protected override Vector2 GetIdealLocalPosition(T element)
        {
            Vector2 lowerLeft = RowAndColumnToPosition(element.Row, element.Column);            
            return new Vector2(lowerLeft.x + _cellWidth/2, lowerLeft.y + _cellHeight/2);
        }

        Vector3 RowAndColumnToPosition(int row, int column)
        {
            return new Vector2(column * _cellWidth - TotalWidth * .5f, row * _cellHeight - TotalHeight * .5f);   
        }
        
        public Vector2[] GetSquarePoints(int gridIndex)
        {
            var offsetCombinations = new Vector2
            (
                Grid.ColumnOfIndex(gridIndex) * CellWidth,
                Grid.RowOfIndex(gridIndex) * CellHeight
            ).GetOffsetCombinations(CellWidth, CellHeight);
            
            return offsetCombinations;
        }	
    }
}
