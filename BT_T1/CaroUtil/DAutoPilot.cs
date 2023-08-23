using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT_T1
{
    public class DAutoPilot
    {
        public class Move
        {
            public int row, col;
        };
        private int boardLevel;
        private int winningCondition;
        private static readonly int emptyCell = 0;
        public DAutoPilot(int boardLevel, int winningCondition)
        {
            this.boardLevel = boardLevel;
            this.winningCondition = winningCondition;
        }

        int player = 1, opponent = 2;

        public Boolean isMovesLeft(int[,] board)
        {
            for (int i = 0; i < boardLevel; i++)
                for (int j = 0; j < boardLevel; j++)
                    if (board[i, j] == emptyCell)
                        return true;
            return false;
        }

        // This is the evaluation function as discussed
        // in the previous article ( http://goo.gl/sJgv68 )
        public int evaluate(int[,] b)
        {
            // Checking for Rows for X or O victory.
            for (int row = 0; row < winningCondition; row++)
            {
                if (b[row, 0] == b[row, 1] &&
                    b[row, 1] == b[row, 2])
                {
                    if (b[row, 0] == player)
                        return +10;
                    else if (b[row, 0] == opponent)
                        return -10;
                }
            }

            // Checking for Columns for X or O victory.
            for (int col = 0; col < winningCondition; col++)
            {
                if (b[0, col] == b[1, col] &&
                    b[1, col] == b[2, col])
                {
                    if (b[0, col] == player)
                        return +10;

                    else if (b[0, col] == opponent)
                        return -10;
                }
            }

            // Checking for Diagonals for X or O victory.
            if (b[0, 0] == b[1, 1] && b[1, 1] == b[2, 2])
            {
                if (b[0, 0] == player)
                    return +10;
                else if (b[0, 0] == opponent)
                    return -10;
            }

            if (b[0, 2] == b[1, 1] && b[1, 1] == b[2, 0])
            {
                if (b[0, 2] == player)
                    return +10;
                else if (b[0, 2] == opponent)
                    return -10;
            }

            // Else if none of them have won then return 0
            return 0;
        }

        // This is the minimax function. It considers all
        // the possible ways the game can go and returns
        // the value of the board
        public int minimax(int[,] board,
                           int depth, Boolean isMax)
        {
            int score = evaluate(board);

            if (score == 10)
                return score;

            if (score == -10)
                return score;

            if (isMovesLeft(board) == false)
                return 0;

            // If this maximizer's move
            if (isMax)
            {
                int best = -1000;

                // Traverse all cells
                for (int i = 0; i < boardLevel; i++)
                {
                    for (int j = 0; j < boardLevel; j++)
                    {
                        // Check if cell is empty
                        if (board[i, j] == emptyCell)
                        {
                            // Make the move
                            board[i, j] = player;

                            // Call minimax recursively and choose
                            // the maximum value
                            best = Math.Max(best, minimax(board,
                                            depth + 1, !isMax));

                            // Undo the move
                            board[i, j] = emptyCell;
                        }
                    }
                }
                return best;
            }

            // If this minimizer's move
            else
            {
                int best = 1000;

                // Traverse all cells
                for (int i = 0; i < boardLevel; i++)
                {
                    for (int j = 0; j < boardLevel; j++)
                    {
                        // Check if cell is empty
                        if (board[i, j] == emptyCell)
                        {
                            // Make the move
                            board[i, j] = opponent;

                            // Call minimax recursively and choose
                            // the minimum value
                            Console.WriteLine("+1 level");
                            best = Math.Min(best, minimax(board,
                                            depth + 1, !isMax));

                            // Undo the move
                            board[i, j] = 0;
                        }
                    }
                }
                return best;
            }
        }

        // This will return the best possible
        // move for the player
        public Move findBestMove(int[,] board)
        {
            int bestVal = -1000;
            Move bestMove = new Move();
            bestMove.row = -1;
            bestMove.col = -1;

            for (int i = 0; i < boardLevel; i++)
            {
                for (int j = 0; j < boardLevel; j++)
                {
                    // Check if cell is empty
                    if (board[i, j] == emptyCell)
                    {
                        // Make the move
                        board[i, j] = player;

                        // compute evaluation function for this
                        // move.
                        int moveVal = minimax(board, 0, false);

                        // Undo the move
                        board[i, j] = emptyCell;

                        // If the value of the current move is
                        // more than the best value, then update
                        // best/
                        if (moveVal > bestVal)
                        {
                            bestMove.row = i;
                            bestMove.col = j;
                            bestVal = moveVal;
                        }
                    }
                }
            }

            Console.Write("The value of the best Move " +
                                "is : {0}\n\n", bestVal);

            return bestMove;
        }
    }
}
