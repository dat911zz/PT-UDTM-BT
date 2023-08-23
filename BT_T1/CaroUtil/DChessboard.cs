using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace BT_T1
{
    public class DChessboard
    {
        private static int _WINCOUNT = 5;
        static readonly int _MAXTIME = 10;

        int boardLevel;
        List<DCell> listCell;
        int[,] board; 
        Form frm;
        Label lblTurn, lblTimeLeft;
        ProgressBar timerBar;
        CellState turn;
        DAutoPilot bot;
        Timer t;
        EventHandler timerHander;
        int counter = _MAXTIME;
        public DChessboard(Form frm, int boardLevel, int firstPlayer)
        {
            this.frm = frm;
            this.boardLevel = boardLevel;
            listCell = new List<DCell>();
            t = new Timer();
            turn = firstPlayer == 1 ? CellState.X : CellState.O;            
            bot = new DAutoPilot(boardLevel, 5);
            timerHander = new EventHandler(delegate (object o, EventArgs e) {
                TimeSpan time = TimeSpan.FromSeconds(counter);
                lblTimeLeft.Text = time.ToString(@"hh\:mm\:ss");
                timerBar.Value = counter;
                if (counter == 0)
                {
                    t.Stop();
                    frm.Refresh();
                    showWinner(turn == CellState.O ? "X" : "O");
                }
                counter--;
            });

            foreach (Control item in frm.Controls)
            {
                if (item.GetType() == typeof(Label))
                {
                    Label lbl = (Label)item;
                    switch (lbl.Name)
                    {
                        case "lblTurn":
                            lblTurn = lbl;
                            break;
                        case "lblTimeLeft":
                            lblTimeLeft = lbl;
                            break;
                        default:
                            break;
                    }
                }
                if (item.GetType() == typeof(ProgressBar))
                {
                    timerBar = (ProgressBar)item;
                }
            }
        }
        public void initComponents(){
            board = new int[boardLevel, boardLevel];
            frm.AutoSize = true;
            int topPos = 10;
            int leftPos = 10;
            int cellSize = 30;

            lblTurn.Text = turn == CellState.O ? "O" : "X";
            scoreBoardLoad();
            for (int i = 0; i < boardLevel; i++)
            {
                leftPos = 10;
                for (int j = 0; j < boardLevel; j++)
                {
                    DCell cell = new DCell(cellSize, cellSize, i , j);
                    cell.Top = topPos;
                    cell.Left = leftPos += 30;
                    cell.Click += new EventHandler(delegate(Object o, EventArgs a)
                    {
                        if (string.IsNullOrEmpty(cell.Text))
                        {
                            cell.mark(turn);
                            board[cell.x, cell.y] = (int)turn;
                            switch (turn)
                            {
                                case CellState.X:                                   
                                    turn = CellState.O;
                                    lblTurn.Text = "O";
                                    break;
                                case CellState.O:
                                    turn = CellState.X;
                                    lblTurn.Text = "X";
                                    break;
                            }
                            scoreBoardLoad();
                            if (isWinner(cell))
                            {
                                Console.WriteLine("Player " + cell.Text + " win");
                                showWinner(cell.Text);
                            }                            
                            //DAutoPilot.Move move = bot.findBestMove(board);
                            //Console.WriteLine(move.row + ":" + move.col);
                            // -- Đợi lâu quá nên dẹp :(( --
                            // Thuật toán Minimax => tối ưu cho ma trận 3x3
                            // Ma trận 10x10 => giải dc nhưng thời gian rất rất lâu để giải dc 1 cell =))
                        }                      
                    });
                    frm.Controls.Add(cell);
                    listCell.Add(cell);
                }
                topPos += cellSize;
            }
        }
        private void showWinner(string winner)
        {
            MessageBox.Show("Chúc mừng người chơi " + winner + " đã chiến thắng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            resetBoard();
        }
        private void resetBoard()
        {
            foreach (Control item in frm.Controls)
            {
                if (item.GetType() == typeof(DCell))
                {
                    DCell btn = (DCell)item;
                    btn.Text = "";
                }
            }
            listCell.Clear();
            initComponents();
            frm.Validate();
        }
        private void scoreBoardLoad()
        {
            t.Tick -= timerHander;
            counter = _MAXTIME;
            timerBar.Value = timerBar.Maximum = counter;
            t.Tick += timerHander;
            t.Interval = 1000;
            t.Start();
            frm.Refresh();
        }
        private bool isEndHorizontal(DCell currentCell)
        {
            int countLeft = 0;
            for (int i = currentCell.x; i >= 0; i--)
            {
                if (board[currentCell.x, i] == (int)currentCell.state)
                {
                    countLeft++;
                }
                else
                    break;
            }

            int countRight = 0;
            for (int i = currentCell.x + 1; i < boardLevel; i++)
            {
                if (board[currentCell.x, i] == (int)currentCell.state)
                {
                    countRight++;
                }
                else
                    break;
            }

            return countLeft + countRight == 5;
        }
        private bool isEndVertical(DCell currentCell)
        {
            int countTop = 0;
            for (int i = currentCell.y; i >= 0; i--)
            {
                if (board[i, currentCell.y] == (int)currentCell.state)
                {
                    countTop++;
                }
                else
                    break;
            }

            int countBottom = 0;
            for (int i = currentCell.y + 1; i < boardLevel; i++)
            {
                if (board[i, currentCell.y] == (int)currentCell.state)
                {
                    countBottom++;
                }
                else
                    break;
            }

            return countTop + countBottom == 5;
        }
        private bool isEndPrimary(DCell currentCell)
        {
            int countTop = 0;
            for (int i = 0; i <= currentCell.x; i++)
            {
                if (currentCell.x - i < 0 || currentCell.y - i < 0)
                    break;

                if (board[currentCell.x - i, currentCell.y - i] == (int)currentCell.state)
                {
                    countTop++;
                }
                else
                    break;
            }

            int countBottom = 0;
            for (int i = 1; i <= boardLevel - currentCell.x; i++)
            {
                if (currentCell.x + i >= boardLevel || currentCell.y + i >= boardLevel)
                    break;

                if (board[currentCell.x + i, currentCell.y + i] == (int)currentCell.state)
                {
                    countBottom++;
                }
                else
                    break;
            }

            return countTop + countBottom == 5;
        }
        private bool isEndSub(DCell currentCell)
        {
            int countTop = 0;
            for (int i = 0; i <= currentCell.x; i++)
            {
                if (currentCell.y + i >= boardLevel || currentCell.x - i < 0)
                    break;

                if (board[currentCell.x - i, currentCell.y + i] == (int)currentCell.state)
                {
                    countTop++;
                }
                else
                    break;
            }

            int countBottom = 0;
            for (int i = 1; i <= boardLevel - currentCell.x; i++)
            {
                if (currentCell.x + i >= boardLevel || currentCell.y - i < 0)
                    break;

                if (board[currentCell.x + i, currentCell.y - i] == (int)currentCell.state)
                {
                    countBottom++;
                }
                else
                    break;
            }

            return countTop + countBottom == 5;
        }
        public bool isWinner(DCell currentCell)
        {
            if (currentCell != null)
            {
                Console.WriteLine();
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        switch ((CellState)board[i,j])
                        {
                            case CellState.X:
                                Console.Write("[{0}]", "X");
                                break;
                            case CellState.O:
                                Console.Write("[{0}]", "O");
                                break;
                            default:
                                Console.Write("[{0}]", " ");
                                break;
                        }
                    }
                    Console.WriteLine();
                }
                if (isEndHorizontal(currentCell) || isEndVertical(currentCell) || isEndPrimary(currentCell) || isEndSub(currentCell))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
