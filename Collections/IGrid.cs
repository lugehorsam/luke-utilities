public interface IGrid {
	
	int Rows { get; }
	int Columns { get; }
	int GetMaxIndex();
	int ToIndex(int row, int col);
	int RowOfIndex(int index);
	int ColumnOfIndex(int index);

}
