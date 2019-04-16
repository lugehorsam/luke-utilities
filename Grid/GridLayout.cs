namespace Utilities.Grid
{
	using System.Collections;

	using UnityEngine;

	/// <summary>
	///     Arbitrary grid layout. Bottom left is row 0, col 0
	/// </summary>
	public class GridLayout : IEnumerable
	{
		private float _cellHeight = default(float);
		private float _cellWidth = default(float);
		private int _columns = 0;
		private int _rows = 0;
		private Transform parent;

		public Grid<GameObject> Grid { get; private set; }

		public float CellWidth
		{
			get { return _cellWidth; }
		}

		public float CellHeight
		{
			get { return _cellHeight; }
		}

		private float _TotalHeight
		{
			get { return _cellHeight * Grid.Rows; }
		}

		private float _TotalWidth
		{
			get { return _cellWidth * Grid.Columns; }
		}

		public GridLayout(Transform parent)
		{
			this.parent = parent;
			Grid = new Grid<GameObject>(_rows, _columns);
		}

		public IEnumerator GetEnumerator()
		{
			return Grid.GetEnumerator();
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
			member.transform.SetParent(parent, true);

			GridCell gridPos = Grid.GetGridCell(member);

			Vector3 idealLocalPos = GetLocalPosition(new GridCell(gridPos.Row, gridPos.Column));

			var asGridMember = member.GetComponentWithInterface<IGridMember>();

			if (asGridMember == null)
			{
				member.transform.localPosition = idealLocalPos;
			}
			else
			{
				asGridMember.SetPosition(idealLocalPos);
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
