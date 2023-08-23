using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace BT_T1
{
    public partial class FrmBai3 : Form
    {
        public FrmBai3()
        {
            InitializeComponent();
            B3.Controller.Instance.loadComponents(this, new Bai3Binding(
                dtpNS, cboEthnic, btnReset, btnSave, listBoxCity, txtName, txtMail, txtPhone    
            ));
        }
        public class Bai3Binding
        {
            public DateTimePicker dtpNS;
            public ComboBox cboEthnic;
            public Button btnReset;
            public Button btnSave;
            public ListBox listBoxCity;
            public TextBox txtName;
            public TextBox txtMail;
            public TextBox txtPhone;

            public Bai3Binding(DateTimePicker dtpNS, ComboBox cboEthric, Button btnReset, Button btnSave, ListBox listBoxCity, TextBox txtName, TextBox txtMail, TextBox txtPhone)
            {
                this.dtpNS = dtpNS;
                this.cboEthnic = cboEthric;
                this.btnReset = btnReset;
                this.btnSave = btnSave;
                this.listBoxCity = listBoxCity;
                this.txtName = txtName;
                this.txtMail = txtMail;
                this.txtPhone = txtPhone;
            }
        }
    }
}
