namespace Utilities
{
    using UnityEngine;
    
    /// <summary>
    /// Arbitrary grid layout. Bottom left is row 0, col 0
    /// </summary>
    public class GridLayout : MonoBehaviour
    {
        private readonly Grid<IGridMember> _grid = new Grid<IGridMember>();
        
        private float _TotalHeight => _cellHeight * _grid.Rows;
        private float _TotalWidth => _cellWidth * _grid.Columns;

        [SerializeField] private float _cellWidth;
        [SerializeField] private float _cellHeight;

        private void Start()
        {
            foreach (var member in _grid)
            {
            }
        }
        
        protected Vector2 GetIdealLocalPosition(IGridMember element)
        {
            Vector2 lowerLeft = RowAndColumnToPosition(element.Row, element.Column);            
            return new Vector2(lowerLeft.x + _cellWidth/2, lowerLeft.y + _cellHeight/2);
        }

        Vector3 RowAndColumnToPosition(int row, int column)
        {
            return new Vector2(column * _cellWidth - _TotalWidth * .5f, row * _cellHeight - _TotalHeight * .5f);   
        }
        
        public Vector2[] GetSquarePoints(int gridIndex)
        {
            var offsetCombinations = new Vector2
            (
                _grid.GetColumnOfIndex(gridIndex) * _cellWidth,
                _grid.GetRowOfIndex(gridIndex) * _cellHeight
            ).GetOffsetCombinations(_cellWidth, _cellHeight);
            
            return offsetCombinations;
        }                
    }
}
