using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT_T1
{
    public class DCell : Button
    {
        public CellState state;
        public int x, y;
        public DCell(int w, int h, int x, int y)
        {
            this.Width = w;
            this.Height = h;
            this.x = x;
            this.y = y;
        }
        public void mark(CellState player)
        {
            switch (player)
            {
                case CellState.X:
                    this.Text = "X";
                    this.state = CellState.X;
                    break;
                case CellState.O:
                    this.Text = "O";
                    this.state = CellState.O;
                    break;
            }
        }
    }
    public enum CellState{
        X = 1,
        O = -1
    }
}
