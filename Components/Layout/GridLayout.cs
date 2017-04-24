using UnityEngine;

namespace Utilities
{

    /// <summary>
    /// Arbitrary grid layout. Bottom left is row 0, col 0
    /// </summary>
    public class GridLayout<T> : Layout<T> where T : IGridMember<T>, ILayoutMember
    {
        private readonly Grid<T> _grid;
        private readonly RectTransform _rectTransform;
        private readonly float _cellWidth;
        private readonly float _cellHeight;

        public GridLayout(Grid<T> grid, float cellWidth, float cellHeight) : base(grid)
        {
            _grid = grid;
            _rectTransform = GameObject.AddComponent<RectTransform>();
            _cellWidth = cellWidth;
            _cellHeight = cellHeight;
            DoLayout();
        }      

        protected override Vector2 GetIdealLocalPosition(T element)
        {
            Debug.Log("column " + element.Column + " and cell width " + _cellWidth);
            return new Vector2(element.Column * _cellWidth, element.Row * _cellHeight);
        }
    }   
}
