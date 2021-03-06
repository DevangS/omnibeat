﻿using System;
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
            Brush color = Brushes.DarkTurquoise;
            Brush white = Brushes.White;
            Brush black = Brushes.Black;
            Brush trans = Brushes.Transparent;

            for (int r = 0; r < row; r++)
            {
                grid[col, r].Opacity = 0;
                grid[col, r].Fill = trans;
                grid[col, r].Stroke = trans;
                state[BeatMaker.chosenButton, col, r] = false;
            }

            grid[col, row].Opacity = 100;
            grid[col, row].Fill = color;
            grid[col, row].Stroke = color;
            state[BeatMaker.chosenButton, col, row] = true;

            for (int r = row + 1; r < maxRow; r++)
            {
                grid[col, r].Opacity = 50;
                grid[col, r].Fill = Brushes.MediumTurquoise;
                grid[col, r].Stroke = trans;
                state[BeatMaker.chosenButton, col, r] = false;
            }

        }


    }
}
