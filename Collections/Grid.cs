using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class Grid<T> : ObservableCollection<T>, IGrid where T : IGridMember
    {                
        public int Rows
        {
            get { return _rows; }
        }
        
        readonly int _rows;

        public int Columns
        {
            get { return _columns; }
        }

        readonly int _columns;

        public Grid() : base(new List<T>())
        {
           
        }
        
        public Grid(int rows, int columns) : base (new List<T>())
        {           
            _rows = rows;
            _columns = columns;
        }

        public T GetElementWithIndex(int index)
        {
            return Items.FirstOrDefault(member => ToIndex(member.Row, member.Column) == index);
        }

        protected sealed override void HandleAfterItemAdd(T item)
        {
            item.Grid = this;
            ValidateMembers();
        }
        
        protected sealed override void HandleAfterItemRemove(T item)
        {
            item.Grid = null;
            ValidateMembers();
        }       
        
        void ValidateMembers()
        {
            var asList = (List<T>) Items;
            
            asList.Sort((element1, element2) => element1.Index.CompareTo(element2.Index));
            
            foreach (var member in this)
            {
                ValidateMember(member);
            }            
        }

        void ValidateMember(T member)
        {
            if (member.Column >= _columns || member.Row >= _rows)
            {
                throw new Exception(string.Format("Invalid member {0} rows and columns of grid {1}, {2}", member, _rows, _columns));
            }
        }
    
        public int GetMaxIndex()
        {
            return GridExt.GetMaxIndex(_rows, _columns);
        }
       
        public int ToIndex(int row, int col) 
        {
            return GridExt.ToIndex(_rows, _columns, row, col);
        }
 
        int[] ToRowCol(int index) {
            return new int[2]{ GetRowOfIndex(index), GetColumnOfIndex(index)};
        }

        int[] RowColOf(T startElement)
        {
            int index = startElement.Index;
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
                adjacentElements.Add 
                (
                    this[ToIndex(row, leftCol)]
                );                
            } 

            if (rightCol < Columns) {
                adjacentElements.Add 
                (
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
        
        public int GetRowOfIndex(int index) {
            return (int) Mathf.Floor (index / _columns);
        }
        
        public int GetColumnOfIndex(int index) {
            return index % _columns;
        }

        public override string ToString()
        {
            return this.ToString(_rows, _columns, this.ToFormattedString());
        }
    }
}
