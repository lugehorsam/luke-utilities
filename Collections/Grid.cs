using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Grid<T> : ObservableList<T> {

	public int Rows {
		get;
		private set;
	}

	public int Cols {
		get;
		private set;
	}
        
    public Grid(int rows, int cols, T[] elements) {
        Rows = rows;
        Cols = cols;
        AddRange (elements);
    }

    public Grid(int rows, int cols) {
        Rows = rows;
        Cols = cols;
    }

    		
	public void Add(int row, int col, T element) {
		this[ToIndex (row, col)] = element;
	}

    public new void Add(T element) {
        base.Add (element);
    }

	public T Peek(int row, int col) {
        return Peek (ToIndex (row, col));
	}

	public T Peek(int index) {
		return this [index];
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
            adjacentElements.Add (Peek (row, leftCol));                
        } 

        if (rightCol < Cols) {
            adjacentElements.Add (Peek (row, rightCol));                
        }

        if (bottomRow >= 0) {
            adjacentElements.Add (Peek (bottomRow, col));                           
        }

        if (topRow < Rows) {
            adjacentElements.Add(Peek(topRow, col));
        }

        return adjacentElements.ToArray();
    }		

    int[] ToRowCol(int index) {
        return new int[2]{ RowOfIndex(index), ColOfIndex(index)};
    }

    int[] RowColOf(T startElement) {
        return ToRowCol (IndexOf (startElement));
    }

    int RowOfIndex(int index) {
        return (int) Mathf.Floor (index / Rows);
    }

    int ColOfIndex(int index) {
        return index % Cols;
    }

	int ToIndex(int row, int col) {
        return row * Cols + col;
	}        
}
