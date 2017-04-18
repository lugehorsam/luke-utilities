using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class Grid<T> where T : IGridMember<T>
    {

        private readonly List<T> members;

        public int Rows
        {
            get { return rows; }
        }
    
        int rows;

        public int Columns
        {
            get { return columns; }
        }

        protected int columns;

        public Grid(int rows, int columns, List<T> members)
        {           
            this.rows = rows;
            this.columns = columns;
            this.members = members;
            ValidateMembers();
        }

        void ValidateMembers()
        {
            foreach (var member in members)
            {
                ValidateMember(member);
            }
        }

        void ValidateMember(T member)
        {
            if (member.Column >= columns || member.Row >= rows)
            {
                throw new Exception(string.Format("Invalid member {0}", member));
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
            return new int[2]{ RowOfIndex(index), ColOfIndex(index)};
        }

        int[] RowColOf(T startElement)
        {
            int index = members.IndexOf(startElement);
            if (index < 0)
            {
                throw new Exception("Grid does not contain element " + startElement + " , grid " + members.ToFormattedString());
            }
            return ToRowCol (index);
        }

        public int RowOfIndex(int index) {
            return (int) Mathf.Floor (index / columns);
        }

        public int ColOfIndex(int index) {
            return index % columns;
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
                    members[ToIndex(row, leftCol)]
                );                
            } 

            if (rightCol < Columns) {
                adjacentElements.Add (
                    members[ToIndex(row, rightCol)]
                );                            
            }

            if (bottomRow >= 0) {
                adjacentElements.Add (
                    members[ToIndex(bottomRow, col)]
                );                           
            }

            if (topRow < Rows) {
                adjacentElements.Add(members[ToIndex(topRow, col)]);
            }

            return adjacentElements.ToArray();
        }		
    }
}
