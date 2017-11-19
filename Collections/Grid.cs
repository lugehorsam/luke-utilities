namespace Utilities.Collections
{
    using System;
    using System.Collections.Generic;

    using UnityEngine;

    public class Grid<T>
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

        public void Set(T item, int row, int column)
        {
            _items[ToIndex(row, column)] = item;
        }

        public T Get(int row, int column)
        {
            return _items[ToIndex(row, column)];
        }

        public int GetMaxIndex()
        {
            return GridExt.GetMaxIndex(Rows, Columns);
        }

        private int ToIndex(int row, int col)
        {
            return GridExt.ToIndex(Rows, Columns, row, col);
        }

        public GridPosition GetGridPosition(int index)
        {
            return new GridPosition {Row = GetRowOfIndex(index), Column = GetColumnOfIndex(index)};
        }

        public GridPosition GetGridPosition(T element)
        {
            return GetGridPosition(Array.IndexOf(_items, element));
        }

        public T[] GetAdjacentElements(T element)
        {
            var adjacentElements = new List<T>();

            GridPosition gridPosition = GetGridPosition(Array.IndexOf(_items, element));

            int row = gridPosition.Row;
            int col = gridPosition.Column;
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
    }
}
