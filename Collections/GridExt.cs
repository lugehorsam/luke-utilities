using System;
using NUnit.Framework.Interfaces;
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
        
        public static int GetRowOfIndex(int index, int columns) {
            return (int) Mathf.Floor (index / columns);
        }
        
        public static int GetColumnOfIndex(int index, int columns) {
            return index % columns;
        }
        
        public static IGridMember GetProjectedMember(IGrid sourceGrid, IGrid gridToProjectOnto, IGridMember memberToProject)
        {
            if (memberToProject.Index > gridToProjectOnto.GetMaxIndex())
            {
                throw new ArgumentOutOfRangeException();
            }            

            var projectedMember = new GridMember(memberToProject.Row, memberToProject.Column);
            projectedMember.Grid = gridToProjectOnto;

            return projectedMember;
        }

        public static IGridMember GetProjectedMember(IGrid sourceGrid, IGrid gridtoProjectOnto, int memberIndex)
        {
            return GetProjectedMember(sourceGrid, gridtoProjectOnto, new GridMember(memberIndex, gridtoProjectOnto));
        }
    }
}
