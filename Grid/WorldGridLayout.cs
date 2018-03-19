namespace Utilities.Grid
{
    using System.Collections;

    using UnityEngine;

    /// <summary>
    ///     Arbitrary grid layout. Bottom left is row 0, col 0
    /// </summary>
    public class WorldGridLayout : MonoBehaviour, IEnumerable
    {
        [SerializeField] private float _cellHeight;

        [SerializeField] private float _cellWidth;
        [SerializeField] private int _columns;

        [SerializeField] private int _rows;

        public Grid<GameObject> Grid { get; private set; }

        private float _TotalHeight
        {
            get { return _cellHeight * Grid.Rows; }
        }

        private float _TotalWidth
        {
            get { return _cellWidth * Grid.Columns; }
        }

        public float CellWidth
        {
            get { return _cellWidth; }
        }

        public float CellHeight
        {
            get { return _cellHeight; }
        }

        public IEnumerator GetEnumerator()
        {
            return Grid.GetEnumerator();
        }

        private void Awake()
        {
            Grid = new Grid<GameObject>(_rows, _columns);
        }

        public void UpdateLayout()
        {
            foreach (GameObject member in Grid)
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

            GridCell gridPos = Grid.GetGridCell(member);

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
            return new Vector2(lowerLeft.x + _cellWidth / 2, lowerLeft.y + _cellHeight / 2);
        }

        public Vector3 GetLocalPosition(int index)
        {
            return GetLocalPosition(Grid.GetGridCell(index));
        }

        private Vector3 RowAndColumnToPosition(int row, int column)
        {
            return new Vector2(column * _cellWidth - _TotalWidth * .5f, row * _cellHeight - _TotalHeight * .5f);
        }

        public Vector2[] GetSquarePoints(int gridIndex)
        {
            Vector2[] offsetCombinations = new Vector2(Grid.GetColumnOfIndex(gridIndex) * _cellWidth, Grid.GetRowOfIndex(gridIndex) * _cellHeight).GetOffsetCombinations(_cellWidth, _cellHeight);

            return offsetCombinations;
        }
    }
}
