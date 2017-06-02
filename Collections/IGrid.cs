public interface IGrid {
	
	int Rows { get; }
	int Columns { get; }
	int GetMaxIndex();
	int ToIndex(int row, int col);
	int GetRowOfIndex(int index);
	int GetColumnOfIndex(int index);

}
