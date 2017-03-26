using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Utilities
{
    [Serializable]
    public class Grid<T> : ISerializationCallbackReceiver where T : IGridMember<T>, new() 
    {   
        public T[] Elements
        {
            get { return processedElements; }
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
                int row = ToRowCol(i)[0];
                int col = ToRowCol(i)[1];
                
                T serializedMember = elements.FirstOrDefault(member => member != null && member.Row == row && member.Column == col);
                
                if (serializedMember == null)
                {
                    Diagnostics.Log("Creating new");
                    serializedMember = new T();
                }

                serializedMember.Row = row;
                serializedMember.Column = col;
                processedElements[i] = serializedMember;
                processedElements[i].Grid = this;
            }
            
            Diagnostics.Log("total processed elements is " + processedElements.Length);
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

        int[] RowColOf(T startElement)
        {                
            int index = Array.IndexOf(processedElements, startElement);
            if (index < 0)
            {
                throw new Exception("Grid does not contain element " + startElement + " , grid " + processedElements.ToFormattedString());
            }
            return ToRowCol (index);
        }

        public int RowOfIndex(int index) {
            return (int) Mathf.Floor (index / rows);
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
                    processedElements[ToIndex(row, leftCol)]
                );                
            } 

            if (rightCol < Columns) {
                adjacentElements.Add (
                    processedElements[ToIndex(row, rightCol)]
                );                            
            }

            if (bottomRow >= 0) {
                adjacentElements.Add (
                    processedElements[ToIndex(bottomRow, col)]
                );                           
            }

            if (topRow < Rows) {
                adjacentElements.Add(processedElements[ToIndex(topRow, col)]);
            }

            return adjacentElements.ToArray();
        }		
    }
}