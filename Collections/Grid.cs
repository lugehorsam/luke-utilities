using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    [Serializable]
    public class Grid<T> : ISerializationCallbackReceiver where T : IGridMember<T>, new() 
    {   
        public ObservableList<T> Members
        {
            get { return processedMembers; }
        }

        private ObservableList<T> processedMembers;

        public ReadOnlyCollection<T> SerializedElements
        {
            get { return new ReadOnlyCollection<T>(elements); }
        }
    
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
            processedMembers = new ObservableList<T>();
            for (int i = 0; i <= maxIndex; i++)
            {
                int row = ToRowCol(i)[0];
                int col = ToRowCol(i)[1];
                
                T serializedMember = elements.FirstOrDefault(member => member != null && member.Row == row && member.Column == col);
                
                if (serializedMember == null)
                {
                    serializedMember = new T();
                }

                serializedMember.Row = row;
                serializedMember.Column = col;
                processedMembers.Add(serializedMember);
                processedMembers[i].Grid = this;
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

        int[] RowColOf(T startElement)
        {
            int index = processedMembers.IndexOf(startElement);
            if (index < 0)
            {
                throw new Exception("Grid does not contain element " + startElement + " , grid " + processedMembers.ToFormattedString());
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
                    processedMembers[ToIndex(row, leftCol)]
                );                
            } 

            if (rightCol < Columns) {
                adjacentElements.Add (
                    processedMembers[ToIndex(row, rightCol)]
                );                            
            }

            if (bottomRow >= 0) {
                adjacentElements.Add (
                    processedMembers[ToIndex(bottomRow, col)]
                );                           
            }

            if (topRow < Rows) {
                adjacentElements.Add(processedMembers[ToIndex(topRow, col)]);
            }

            return adjacentElements.ToArray();
        }		
    }
}