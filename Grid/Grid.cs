namespace Utilities.Grid
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Grid<T> : IEnumerable
    {
        public int Rows { get; }

        public int Columns { get; }

        private readonly T[] _items;

        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            _items = new T[GetMaxIndex() + 1];
        }
        
        public void Set(T item, GridCell gridCell)
        {
            _items[ToIndex(gridCell.Row, gridCell.Column)] = item;
        }

        public void Set(int index, T item)
        {
            _items[index] = item;
        }

        public bool HasWithinBounds(GridCell gridCell)
        {
            return gridCell.Row < Rows && gridCell.Row >= 0 && 
                   gridCell.Column < Columns && gridCell.Column >= 0;
        }

        public T Get(GridCell gridCell)
        {
            return _items[ToIndex(gridCell)];
        }

        public int GetMaxIndex()
        {
            return GridExt.GetMaxIndex(Rows, Columns);
        }

        private int ToIndex(int row, int col)
        {
            return GridExt.ToIndex(Rows, Columns, row, col);
        }

        private int ToIndex(GridCell gridCell)
        {
            return ToIndex(gridCell.Row, gridCell.Column);
        }

        public GridCell GetGridCell(int index)
        {
            return new GridCell {Row = GetRowOfIndex(index), Column = GetColumnOfIndex(index)};
        }

        public GridCell GetGridCell(T element)
        {
            return GetGridCell(Array.IndexOf(_items, element));
        }

        public T[] GetAdjacentElements(T element)
        {
            var adjacentElements = new List<T>();

            GridCell gridCell = GetGridCell(Array.IndexOf(_items, element));

            int row = gridCell.Row;
            int col = gridCell.Column;
            int leftCol = col - 1;
            int rightCol = col + 1;
            int topRow = row + 1;
            int bottomRow = row - 1;

            if (leftCol >= 0)
            {
                adjacentElements.Add(_items[ToIndex(row, leftCol)]);
            }

            if (rightCol < Columns)
            {
                adjacentElements.Add(_items[ToIndex(row, rightCol)]);
            }

            if (bottomRow >= 0)
            {
                adjacentElements.Add(_items[ToIndex(bottomRow, col)]);
            }

            if (topRow < Rows)
            {
                adjacentElements.Add(_items[ToIndex(topRow, col)]);
            }

            return adjacentElements.ToArray();
        }

        public int GetRowOfIndex(int index)
        {
            return (int) Mathf.Floor(index / Columns);
        }

        public int GetColumnOfIndex(int index)
        {
            return index % Columns;
        }

        public IEnumerator GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public GridCell GetPositionOfIndex(int index)
        {
            return new GridCell(GetRowOfIndex(index), GetColumnOfIndex(index));
        }
    }
}
