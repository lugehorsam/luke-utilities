namespace Utilities
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using UnityEngine;
    using Observable;
    
    public class Grid<T> : Observables<T>, IGrid where T : IGridMember
    {                
        public int Rows => _rows;

        readonly int _rows;

        public int Columns => _columns;

        readonly int _columns;

        public Grid() : base(new List<T>())
        {
           
        }
        
        public Grid(int rows, int columns) : base (new List<T>())
        {           
            _rows = rows;
            _columns = columns;
        }

        public T GetElement(int index)
        {
            return Items.FirstOrDefault(member => ToIndex(member.Row, member.Column) == index);
        }

        public T GetElement(int row, int column)
        {
            return Items.FirstOrDefault(member => member.Row == row && member.Column == column);
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
            
            asList.Sort((element1, element2) => ToIndex(element1).CompareTo(ToIndex(element2)));
            
            foreach (var member in this)
            {
                ValidateMember(member);
            }            
        }

        int ToIndex(T element)
        {
            return ToIndex(element.Row, element.Column);
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
            int index = ToIndex(startElement);
            if (index < 0)
            {
                throw new Exception("RuneLevel does not contain element " + startElement + " , grid " + IEnumerableExtensions.Pretty(this));
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
            return this.ToString(_rows, _columns, IEnumerableExtensions.Pretty(this));
        }
    }
}
