using System;
using UnityEngine;

namespace Utilities.Collections
{
    public static class GridExt {

/**        public static int GetManhattanDistanceFrom (IGridMember elementA, IGridMember elementB)
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
        }**/

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

        public static GridPosition? GetFirstEmptyPosition<T>(this Grid<T> thisGrid)
        {
            var enumerator = thisGrid.GetEnumerator();

            int i = 0;
            
            do
            {
                if (enumerator.Current == null)
                {
                    Diag.Log("returning at i " + i);
                    return thisGrid.GetPositionOfIndex(i);
                }

                i++;
            }
            while (enumerator.MoveNext());

            return null;
        }
        
       
    }
}
