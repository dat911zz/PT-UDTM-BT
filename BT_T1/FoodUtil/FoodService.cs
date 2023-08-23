using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT_T1
{
    public class FoodService
    {
        public Form frm;
        public List<string> foods;
        Label lblCount = new Label();
        Label lblCountName = new Label();
        Label lblChoose = new Label();
        Label lblChooseName = new Label();
        List<string> choose = new List<string>();

        public FoodService(Form fmr, List<string> foods)
        {
            this.frm = fmr;
            this.foods = foods;
        }
        public void addComponents()
        {
            int topPosition = 10;
            foreach (string food in foods)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Left = 10;
                checkBox.Top = topPosition;
                topPosition += 30;
                checkBox.Text = food;
                checkBox.CheckedChanged += new EventHandler(delegate(Object o, EventArgs a)
                {
                    foodCounter();
                });
                frm.Controls.Add(checkBox);
            }

            lblCountName.Left = 140;
            lblCountName.Top = 50;
            lblCountName.Width = 130;
            lblCountName.Text = "So luong mon da chon: ";
            frm.Controls.Add(lblCountName);

            lblCount.Left = 290;
            lblCount.Top = 50;
            lblCount.Width = 130;
            lblCount.Text = "0";
            frm.Controls.Add(lblCount);

            lblChooseName.Left = 140;
            lblChooseName.Top = 80;
            lblChooseName.Width = 130;
            lblChooseName.Text = "Danh sach mon da chon: ";
            frm.Controls.Add(lblChooseName);

            lblChoose.Left = 290;
            lblChoose.Top = 80;
            lblChoose.AutoSize = true;
            lblChoose.Text = "0";
            frm.Controls.Add(lblChoose);
        }
        public void foodCounter()
        {
            choose.Clear();
            foreach (Control item in frm.Controls)
            {
                if (item.GetType() == typeof(CheckBox))
                {
                    CheckBox cb = (CheckBox)item;
                    if (cb.Checked)
                    {
                        choose.Add(cb.Text);
                    }
                }
            }
            lblCount.Text = choose.Count.ToString();
            lblChoose.Text = String.Join(",",choose);
        }
    }
}
