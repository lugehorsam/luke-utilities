namespace Utilities.Grid
{
    using Utilities;
    
    public struct GridCell
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public GridCell(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public GridCell ApplyDirection(Direction direction)
        {
            int newRow = Row;
            int newCol = Column;
            
            switch (direction)
            {
                case Direction.Up:
                    newRow++;
                    break;
                case Direction.Down:
                    newRow--;
                    break;
                case Direction.Left:
                    newCol--;
                    break;
                case Direction.Right:
                    newCol++;    
                    break;
            }
            
            return new GridCell(newRow, newCol);
        }

        public override string ToString()
        {
            return $"({Row}, {Column})";
        }
    }
}
