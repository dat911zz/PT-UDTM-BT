using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT_T1
{
    public partial class FrmFood : Form
    {
        public FrmFood()
        {
            InitializeComponent();
            LoadComponent();
        }
        public void LoadComponent(){
            List<string> foods = new FoodDB().getFoodList();
            new FoodService(this, foods).addComponents();
        }
    }
}
