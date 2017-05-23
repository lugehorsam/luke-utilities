using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    public class Grid<T> : ObservableCollection<T>, IGrid where T : IGridMember
    {
        public int Rows
        {
            get { return rows; }
        }
    
        [SerializeField]
        int rows;

        public int Columns
        {
            get { return columns; }
        }

        [SerializeField]
        protected int columns;

        public Grid()
        {
            
        }
        
        public Grid(int rows, int columns)
        {           
            this.rows = rows;
            this.columns = columns;
        }

        public T GetElementWithIndex(int index)
        {
            return Items.FirstOrDefault(member => ToIndex(member.Row, member.Column) == index);
        }

        protected sealed override void HandleAfterItemAdd(T item)
        {
            item.Grid = this;
            ValidateMember(item);
        }
        
        protected sealed override void HandleAfterItemRemove(T item)
        {
            item.Grid = null;
            ValidateMembers();
        }       

        void ValidateMembers()
        {
            foreach (var member in this)
            {
                ValidateMember(member);
            }
        }

        void ValidateMember(T member)
        {
            if (member.Column >= columns || member.Row >= rows)
            {
                throw new Exception(string.Format("Invalid member {0} rows and columns of grid {1}, {2}", member, rows, columns));
            }
        }
    
        public int GetMaxIndex()
        {
            return rows * columns - 1;
        }
       
        public int ToIndex(int row, int col) 
        {
            return row * columns + col;
        }
 
        int[] ToRowCol(int index) {
            return new int[2]{ RowOfIndex(index), ColumnOfIndex(index)};
        }

        int[] RowColOf(T startElement)
        {
            int index = IndexOf(startElement);
            if (index < 0)
            {
                throw new Exception("RuneLevel does not contain element " + startElement + " , grid " + this.ToFormattedString());
            }
            return ToRowCol (index);
        }

        public T[] GetAdjacentElements(T startElement) {
          
            List<T> adjacentElements = new List<T> ();
            int[] rowCol = RowColOf (startElement);
            int row = rowCol [0];
            int col = rowCol [1];
            int leftCol = col - 1;
            int rightCol = col + 1;
            int topRow = row + 1;
            int bottomRow = row - 1;

            if (leftCol >= 0) {
                adjacentElements.Add (
                    this[ToIndex(row, leftCol)]
                );                
            } 

            if (rightCol < Columns) {
                adjacentElements.Add (
                    this[ToIndex(row, rightCol)]
                );                            
            }

            if (bottomRow >= 0) {
                adjacentElements.Add (
                    this[ToIndex(bottomRow, col)]
                );                           
            }

            if (topRow < Rows) {
                adjacentElements.Add(this[ToIndex(topRow, col)]);
            }

            return adjacentElements.ToArray();
        }		
        
        public int RowOfIndex(int index) {
            return (int) Mathf.Floor (index / columns);
        }
        
        public int ColumnOfIndex(int index) {
            return index % columns;
        }
    }
}
