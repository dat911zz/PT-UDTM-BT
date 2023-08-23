using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT_T1
{
    public class DialogUtil
    {
        private static DialogUtil instance;
        public static DialogUtil Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DialogUtil();
                }
                return instance;
            }
        }
        private DialogUtil()
        {

        }
        public bool messageQuestion(string text, string caption)
        {
            var res = MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return res == DialogResult.Yes;
        }
        public void messageError(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void messageWarning(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void messageInfo(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
