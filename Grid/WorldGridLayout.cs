namespace Utilities.Grid
{
    using System.Collections;

    using UnityEngine;
    
    using Utilties;
    
    /// <summary>
    /// Arbitrary grid layout. Bottom left is row 0, col 0
    /// </summary>
    public class WorldGridLayout : MonoBehaviour, IEnumerable
    {
        private Grid<GameObject> _grid;

        public Grid<GameObject> Grid => _grid;
        
        private float _TotalHeight => _cellHeight * _grid.Rows;
        private float _TotalWidth => _cellWidth * _grid.Columns;

        [SerializeField] private float _cellWidth;
        [SerializeField] private float _cellHeight;

        [SerializeField] private int _rows;
        [SerializeField] private int _columns;

        public float CellWidth => _cellWidth;
        public float CellHeight => _cellHeight;

        private void Awake()
        {        
            _grid = new Grid<GameObject>(_rows, _columns);   
        }

        public void UpdateLayout()
        {
            foreach (GameObject member in _grid)
            {
                if (member == null)
                {
                    continue;
                }
                
                UpdateLayout(member);    
            }
        }
 
        private void UpdateLayout(GameObject member)
        {
            member.transform.SetParent(transform, true);

            GridCell gridPos = _grid.GetGridPosition(member);

            Vector3 idealLocalPos = GetLocalPosition(new GridCell(gridPos.Row, gridPos.Column));
                        
            var asGridMember = member.GetComponentWithInterface<IWorldGridMember>();
            
            if (asGridMember == null)
            {
                member.transform.localPosition = idealLocalPos;
            }
            else
            {
                StartCoroutine(asGridMember.SetPosition(idealLocalPos));
            }
        }

        public Vector3 GetLocalPosition(GridCell gridCell)
        {
            Vector2 lowerLeft = RowAndColumnToPosition(gridCell.Row, gridCell.Column);   
            return new Vector2(lowerLeft.x + _cellWidth/2, lowerLeft.y + _cellHeight/2);
        }

        public GridCell GetGridPosition(GameObject member)
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

        public IEnumerator GetEnumerator()
        {
            return _grid.GetEnumerator();
        }
    }
}
