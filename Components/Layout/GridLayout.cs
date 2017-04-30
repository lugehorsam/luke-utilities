using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{

    /// <summary>
    /// Arbitrary grid layout. Bottom left is row 0, col 0
    /// </summary>
    public class GridLayout<T> : Layout<T> where T : IGridMember<T>, ILayoutMember
    {
        public event Action<int> OnCellTouch = delegate { };
        protected override string Name { get { return "GridLayout";  } }

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
        private readonly List<GridCell> _cellOutlines;
        
        public GridLayout(Grid<T> grid, float cellWidth, float cellHeight) : base(grid)
        {
            _grid = grid;
            _rectTransform = GameObject.AddComponent<RectTransform>();
            _cellWidth = cellWidth;
            _cellHeight = cellHeight;
            _cellOutlines = CreateCells();
            DoLayout();
        }

        List<GridCell> CreateCells()
        {
            var lineSquares = new List<GridCell>();
            for (int i = 0; i < _grid.Rows * _grid.Columns; i++)
            {
                Vector2[] squarePoints = GetSquarePoints(0);
                var gridCell = new GridCell
                (
                    squarePoints[0],
                    squarePoints[1],
                    squarePoints[2],
                    squarePoints[3],
                    new Vector3(_cellWidth, _cellHeight)
                );
                
                gridCell.LineWidth = .025f;
                gridCell.Transform.SetParent(Transform);
                gridCell.Transform.localPosition = RowAndColumnToPosition(_grid.RowOfIndex(i), _grid.ColumnOfIndex(i));
                lineSquares.Add
                (
                    gridCell    
                );

                int currIndex = i;
                
                gridCell.TouchDispatcher.OnTouch += (cell, gesture) => OnCellTouch(currIndex);
            }

            return lineSquares;
        }
                
        protected override Vector2 GetIdealLocalPosition(T element)
        {
            return RowAndColumnToPosition(element.Row, element.Column);
        }

        Vector3 RowAndColumnToPosition(int row, int column)
        {
            return new Vector2(column * _cellWidth - TotalWidth * .5f, row * _cellHeight - TotalHeight * .5f);   
        }

        public void ShowOutline(bool show)
        {
            foreach (var outline in _cellOutlines)
            {
                outline.GameObject.SetActive(show);
            }
        }

        Vector2[] GetSquarePoints(int gridIndex)
        {
            var offsetCombinations = new Vector2
            (
                _grid.ColumnOfIndex(gridIndex) * _cellWidth,
                _grid.RowOfIndex(gridIndex) * _cellHeight
            ).GetOffsetCombinations(_cellWidth, _cellHeight);
            
            return offsetCombinations;
        }
    }   
}
