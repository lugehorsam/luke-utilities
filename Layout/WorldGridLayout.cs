namespace Utilities
{
    using Collections;

    using UnityEngine;
    
    /// <summary>
    /// Arbitrary grid layout. Bottom left is row 0, col 0
    /// </summary>
    public class WorldGridLayout : MonoBehaviour
    {
        private Grid<MonoBehaviour> _grid;
        
        private float _TotalHeight => _cellHeight * _grid.Rows;
        private float _TotalWidth => _cellWidth * _grid.Columns;

        [SerializeField] private float _cellWidth;
        [SerializeField] private float _cellHeight;

        public float CellWidth => _cellWidth;
        public float CellHeight => _cellHeight;

        public void Init(int rows, int columns)
        {        
            _grid = new Grid<MonoBehaviour>(rows, columns);   
        }

        public void Add(MonoBehaviour member, int row, int column)
        {
            _grid.Set(member, row, column);

            Vector3 idealLocalPos = GetLocalPosition(new GridPosition(row, column));
                        
            var asGridMember = member as IGridLayoutMember;
            
            if (asGridMember == null)
            {
                member.transform.localPosition = idealLocalPos;
            }
            else
            {
                StartCoroutine(asGridMember.SetPosition(idealLocalPos));
            }
        }

        public Vector3 GetLocalPosition(GridPosition gridPosition)
        {
            Vector2 lowerLeft = RowAndColumnToPosition(gridPosition.Row, gridPosition.Column);   
            return new Vector2(lowerLeft.x + _cellWidth/2, lowerLeft.y + _cellHeight/2);
        }

        public GridPosition GetGridPosition(MonoBehaviour member)
        {
            return _grid.GetGridPosition(member);
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
