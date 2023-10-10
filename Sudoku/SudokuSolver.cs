using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public static class SudokuSolver
    {
        private static int[] _availableNumbers = { 1,2,3,4,5,6,7,8,9 };
        private static Random _rand = new Random();
        private const int _matrixNum = 9;

        public static string InitializeSudoku()
        {
            int[,] grid = GenerateRandomBoard();

            if(FillSudoku(grid, 0, 0))
                return TurnGridToString(grid);

            return string.Empty;
        }


        // This is for the use of the FillSudoku method
        private static int[,] GenerateRandomBoard()
        {
            int[,] board = new int[,] { 
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
            };

            int[] firstRow = GenerateFirstRow();

            // Fill in first row with random order of numbers
            for(int i = 0; i < firstRow.Length; i++)
            {
                board[0, i] = firstRow[i];
            }

            return board;
        }

        private static int[] GenerateFirstRow()
        {
            List<int> row = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] newRow = new int[9];

            for(int i = 0; i < 9; i++)
            {
                int randNum = row[_rand.Next(row.Count)];
                newRow[i] = randNum;

                row.Remove(randNum);
            }


            return newRow;
        }


        #region Credit to geeksforgeeks: https://www.geeksforgeeks.org/sudoku-backtracking-7/

        /* Takes a partially filled-in grid and attempts
          to assign values to all unassigned locations in
          such a way to meet the requirements for
          Sudoku solution (non-duplication across rows,
          columns, and boxes) */
        private static bool FillSudoku(int[,] grid, int row, int col)
        {

            /*if we have reached the 8th
                   row and 9th column (0
                   indexed matrix) ,
                   we are returning true to avoid further
                   backtracking       */
            if (row == _matrixNum - 1 && col == _matrixNum)
                return true;

            // Check if column value  becomes 9 ,
            // we move to next row
            // and column start from 0
            if (col == _matrixNum)
            {
                row++;
                col = 0;
            }

            // Check if the current position
            // of the grid already
            // contains value >0, we iterate
            // for next column
            if (grid[row, col] != 0)
                return FillSudoku(grid, row, col + 1);

            for (int num = 1; num < 10; num++)
            {

                // Check if it is safe to place
                // the num (1-9)  in the
                // given row ,col ->we move to next column
                if (IsSafe(grid, row, col, num))
                {

                    /*  assigning the num in the current
                            (row,col)  position of the grid and
                            assuming our assigned num in the position
                            is correct */
                    grid[row, col] = num;

                    // Checking for next
                    // possibility with next column
                    if (FillSudoku(grid, row, col + 1))
                        return true;
                }
                /* removing the assigned num , since our
                         assumption was wrong , and we go for next
                         assumption with diff num value   */
                grid[row, col] = 0;
            }
            return false;
        }

        /* A utility function to print grid */
        private static string TurnGridToString(int[,] grid)
        {
            string gridStr = "";

            for (int i = 0; i < _matrixNum; i++)
            {
                for (int j = 0; j < _matrixNum; j++)
                    gridStr +=  $"{grid[i, j]}";
            }

            return gridStr;
        }

        // Check whether it will be legal
        // to assign num to the
        // given row, col
        private static bool IsSafe(int[,] grid, int row, int col, int num)
        {

            // Check if we find the same num
            // in the similar row , we
            // return false
            for (int x = 0; x <= 8; x++)
                if (grid[row, x] == num)
                    return false;

            // Check if we find the same num
            // in the similar column ,
            // we return false
            for (int x = 0; x <= 8; x++)
                if (grid[x, col] == num)
                    return false;

            // Check if we find the same num
            // in the particular 3*3
            // matrix, we return false
            int startRow = row - row % 3, startCol
              = col - col % 3;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (grid[i + startRow, j + startCol] == num)
                        return false;

            return true;
        }
        #endregion
    }
}
