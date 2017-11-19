namespace Utilities
{
    public struct GridPosition
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public GridPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public GridPosition ApplyDirection(Direction direction)
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
            
            return new GridPosition(newRow, newCol);
        }

        public override string ToString()
        {
            return $"({Row}, {Column})";
        }
    }
}
