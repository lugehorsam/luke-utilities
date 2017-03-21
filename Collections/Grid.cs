using System;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    [Serializable]
    public class Grid<T> where T : IGridMember<T>, new()
    {   
        public T[] Elements
        {
            get { return elements; }
        }

        private T[] processedElements;
    
        [SerializeField]
        private T[] elements;

        public int Rows
        {
            get { return rows; }
        }
    
        [SerializeField]
        private int rows;

        public int Columns
        {
            get { return columns; }
        }

        [SerializeField]
        private int columns;

        public void OnBeforeSerialize()
        {
        
        }
    
        public virtual void OnAfterDeserialize()
        {
            if (!RowsAndColumnsAreDefined())
            {
                throw new Exception("Rows and columns are not defined!");
            }

            if (elements == null)
            {
                Diagnostics.LogWarning("Elements is null");
            }
        
            SetMemberDataFromSerializedElements();
        }

        void SetMemberDataFromSerializedElements()
        {
            int maxIndex = GetMaxIndex();
            processedElements = new T[maxIndex + 1];
            for (int i = 0; i <= maxIndex; i++)
            {
                bool serializedContainsIndex = processedElements.Any(member => member != null && member.Index == i);
                if (!serializedContainsIndex)
                {
                    processedElements[i] = new T();
                    processedElements[i].Grid = this;
                    processedElements[i].Index = i;
                }
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

        bool RowsAndColumnsAreDefined()
        {
            return rows != 0 && columns != 0;
        }    
    
        int[] ToRowCol(int index) {
            return new int[2]{ RowOfIndex(index), ColOfIndex(index)};
        }

        int[] RowColOf(T startElement) {
            return ToRowCol (Array.IndexOf (processedElements, startElement));
        }

        public int RowOfIndex(int index) {
            return (int) Mathf.Floor (index / rows);
        }

        public int ColOfIndex(int index) {
            return index % columns;
        }
    }
}