using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;

namespace OmniBeat
{
    class PitchSequence
    {
        public int maxRow { get; set; }
        public int maxCol { get; set; }
        public Rectangle[,] grid { get; set; }

        public PitchSequence(Rectangle[,] pgrid)
        {
            // Assume that the pgrid with always be a non-jagged 
            // multi-dimensional array
            if (pgrid == null || pgrid.GetLength(0) <= 0)
            {
                return;
            }

            // Set column length
            maxCol = pgrid.GetLength(0);

            if (pgrid.GetLength(1) <= 0)
            {
                return;
            }

            // Set row length
            maxRow = pgrid.GetLength(1);

            // Create a new grid
            grid = pgrid; ;


            // Populate the grid
            for (int row = 0; row < maxRow; row++)
            {
                for (int col = 0; col < maxCol; col++)
                {
                    grid[col, row] = pgrid[col, row];
                }
            }
        }

        public void Click(int col, int row, ref Boolean[,,] state)
        {
            for (int r = 0; r < row; r++)
            {    
                UnClick(col, r, ref state);  
            }

            for (int r = row; r < maxRow; r++)
            {
                if (r == row)
                {
                    state[MainWindow.chosenButton, col, row] = true;
                    grid[col, r].Stroke = Brushes.Black;
                }
				else
				{
					grid[col, r].Stroke = Brushes.Transparent;
				}
                grid[col, r].Fill = new SolidColorBrush(Colors.DarkSalmon);
                grid[col, r].Opacity = 100;
            }

        }

        public void UnClick(int col, int row, ref Boolean[, ,] state)
        {
            grid[col, row].Fill = new SolidColorBrush(Colors.White);
            grid[col, row].Opacity = 0;
            state[BeatMaker.chosenButton, col, row] = false;
        }
    }
}
