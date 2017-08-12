using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Input;

namespace Utilities
{
    /// <summary>
    /// Arbitrary grid layout. Bottom left is row 0, col 0
    /// </summary>
    public class GridLayout<T> : Layout<T, Grid<T>>, IGridLayout where T : IGridMember, ILayoutMember
    {
        public override string GameObjectName => "Grid Layout";

        IGrid IGridLayout.Grid => Data;

        public float CellHeight => _cellHeight;

        public float CellWidth => _cellWidth;

        public float TotalHeight => CellHeight * Data.Rows;

        private float TotalWidth => CellWidth * Data.Columns;

        private readonly float _cellWidth;
        private readonly float _cellHeight;        
        private readonly RectTransform _rectTransform;

        public GridLayout(float cellWidth, float cellHeight)  : base(new Grid<T>())
        {
            _rectTransform = _GameObject.AddComponent<RectTransform>();
            _cellWidth = cellWidth;
            _cellHeight = cellHeight;
        }        
               
        protected override Vector2 GetIdealLocalPosition(T element)
        {
            Vector2 lowerLeft = RowAndColumnToPosition(element.Row, element.Column);            
            return new Vector2(lowerLeft.x + _cellWidth/2, lowerLeft.y + _cellHeight/2);
        }

        Vector3 RowAndColumnToPosition(int row, int column)
        {
            return new Vector2(column * _cellWidth - TotalWidth * .5f, row * _cellHeight - TotalHeight * .5f);   
        }
        
        public Vector2[] GetSquarePoints(int gridIndex)
        {
            var offsetCombinations = new Vector2
            (
                Data.GetColumnOfIndex(gridIndex) * CellWidth,
                Data.GetRowOfIndex(gridIndex) * CellHeight
            ).GetOffsetCombinations(CellWidth, CellHeight);
            
            return offsetCombinations;
        }
        
        void HandleGridItemAdd(T item)
        {            
            DoLayout();            
        }

        void HandleGridItemRemove(T item)
        {
            DoLayout();   
        }

        protected sealed override void HandleLayoutDatumChanged(Grid<T> oldData, Grid<T> newData)
        {
            if (oldData != null)
            {                
                oldData.OnAfterItemAdd -= HandleGridItemAdd;
                oldData.OnAfterItemRemove -= HandleGridItemRemove;
                                
                foreach (var item in oldData)
                {
                    HandleGridItemRemove(item);
                }
            }

            if (newData != null)
            {                
                newData.OnAfterItemAdd += HandleGridItemAdd;
                newData.OnAfterItemRemove += HandleGridItemRemove;

                foreach (var item in newData)
                {
                    HandleGridItemAdd(item);
                }
            }
        }
        
        
    }
}
