using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT_T1
{
    public partial class Menu : Form
    {
        static readonly Dictionary<string, string> menus = new Dictionary<string, string>()
        {
            { "Caro", "BT_T1.FrmCaro" },
            { "Food", "BT_T1.FrmFood" },
            { "Bài 3", "BT_T1.FrmBai3" }
        };
        public Menu()
        {
            InitializeComponent();
            addMenu();
        }
        public void addMenu()
        {
            int topPosition = 10;
            Label lbl = new Label();
            lbl.Left = 10;
            lbl.Top = topPosition;
            lbl.Width = 130;
            lbl.Text = "Vui lòng chọn bài: ";
            this.Controls.Add(lbl);

            topPosition += 30;
            foreach (var item in menus)
            {
                Button btn = new Button();
                btn.Left = 10;
                btn.Top = topPosition;
                topPosition += 30;
                btn.Text = item.Key;
                btn.Click += new EventHandler(delegate (Object o, EventArgs a)
                {
                    formCaller(menus[((Button)o).Text]).Show();
                });
                this.Controls.Add(btn);
            }
        }
        public Form formCaller(String formName)
        {
            // Get a type from the string 
            Type type = Type.GetType(formName, true);
            // Create an instance of that type
            return (Form)Activator.CreateInstance(type);
        }
    }
}
