using System;
using UnityEngine;

namespace Utilities
{
    public static class GridExt {

        public static int GetManhattanDistanceFrom (IGridMember elementA, IGridMember elementB)
        {
            return Math.Abs (elementA.Row - elementB.Row) + Math.Abs (elementA.Column - elementB.Column);
        }

        /// <summary>
        /// Gets the chebyshev distance. Distance across diagonals are the same distance as adjacent tiles.
        /// </summary>
        /// <returns>The chebyshev distance from.</returns>
        /// <param name="otherOccupant">Other occupant.</param>
        public static int GetChebyshevDistanceFrom(IGridMember elementA, IGridMember elementB) {
            return Math.Max(Math.Abs(elementA.Row - elementB.Row), Math.Abs(elementA.Column - elementB.Column));
        }

        public static int GetMaxIndex(int rows, int columns)
        {
            return rows * columns - 1;
        }

        public static int ToIndex(int rows, int columns, int row, int col)
        {
            return row * columns + col;
        }
        
        public static int GetRowOfIndex(int index, int columns) 
        {
            return (int) Mathf.Floor (index / columns);
        }
        
        public static int GetColumnOfIndex(int index, int columns) 
        {
            return index % columns;
        }
        
        public static IGridMember GetProjectedMember
        (
            IGrid sourceGrid, 
            IGrid gridToProjectOnto, 
            IGridMember memberToProject,            
            int rowOffset = 0,
            int colOffset = 0
        )
        {
            return GetProjectedMember(sourceGrid, gridToProjectOnto, sourceGrid.ToIndex(memberToProject.Row, memberToProject.Column), rowOffset, colOffset);
        }

        public static IGridMember GetProjectedMember
        (
            IGrid sourceGrid, 
            IGrid gridToProjectOnto,
            int indexToProject,
            int rowOffset = 0,
            int colOffset = 0
        )
        {
            int origRow = sourceGrid.GetRowOfIndex(indexToProject);
            int origCol = sourceGrid.GetColumnOfIndex(indexToProject);
                        
            var projectedMember = new GridMember(origRow, origCol);

            projectedMember.Grid = gridToProjectOnto;
                        
            projectedMember.Row += rowOffset;
            projectedMember.Column += colOffset;
            
            if (projectedMember.Index > gridToProjectOnto.GetMaxIndex())
            {
                throw new ArgumentOutOfRangeException();
            }

            return projectedMember;
        }
    }
}
