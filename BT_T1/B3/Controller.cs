using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static BT_T1.FrmBai3;

namespace BT_T1.B3
{
    public class Controller
    {
        private Bai3Binding binding;
        private static Controller instance;
        private static string PATH = Path.Combine(getCurrentRootPath(), Assembly.GetExecutingAssembly().GetName().Name);
        public static Controller Instance { 
            get {
                if (instance == null)
                {
                    instance = new Controller();
                }
                return instance; 
            }
        }
        private Controller()
        {
            
        }
        public static string getCurrentRootPath()
        {
            string[] result = Environment.CurrentDirectory.Split('\\');
            return String.Join(@"\", result.Take(result.Count() - 3).ToArray());
        }
        public static List<string> readFileTxt(string path)
        {
            List<string> result = new List<string>();
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    result.Add(reader.ReadLine());
                }
            }
            return result;
        }
        public static void writeFileTxt(string[] data, string path, bool append = false)
        {
            using (StreamWriter writer = new StreamWriter(path, append))
            {
                foreach (string s in data)
                {
                    writer.WriteLine(s);
                }
            }
        }
        public void loadComponents(Form frm, Bai3Binding binding) { 
            this.binding = binding;
            bindEvents(frm);
        }
        public void bindEvents(Form frm)
        {
            string pathEthnic = PATH + @"\Files\ethnic.txt";
            string pathCity = PATH + @"\Files\city.txt";
            string[] cities = readFileTxt(pathCity).ToArray();
            string[] ethnic = readFileTxt(pathEthnic).ToArray();

            binding.cboEthnic.AutoCompleteCustomSource.AddRange(ethnic);
            binding.cboEthnic.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            binding.cboEthnic.AutoCompleteSource = AutoCompleteSource.CustomSource;
            binding.listBoxCity.Items.AddRange(cities);
            binding.txtName.KeyPress += new KeyPressEventHandler(delegate (object sender, KeyPressEventArgs e)
            {
                if (Regex.IsMatch(e.KeyChar.ToString(), @"[!@#$&*0-9]"))
                {
                    DialogUtil.Instance.messageWarning("Tên không được nhập ký tự đặc biệt và số !", "Cảnh báo");
                    e.Handled = true;
                }
            });
            binding.txtPhone.KeyPress += new KeyPressEventHandler(delegate (object sender, KeyPressEventArgs e)
            {
                if ((int)e.KeyChar != (int)Keys.Back && !Regex.IsMatch(e.KeyChar.ToString(), @"[\d]"))
                {
                    DialogUtil.Instance.messageWarning("SĐT chỉ được nhập số !", "Cảnh báo");
                    e.Handled = true;
                }
            });
            binding.txtMail.Leave += new EventHandler(delegate (Object o, EventArgs e)
            {
                TextBox phoneControl = (TextBox)o;
                if (!Regex.IsMatch(phoneControl.Text, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                {
                    DialogUtil.Instance.messageError("Mail không đúng định dạng!", "Lỗi");
                    phoneControl.Focus();
                }
            });
            binding.btnReset.Click += new EventHandler(delegate (Object o, EventArgs e)
            {
                clearInput(frm);
            });
            binding.btnSave.Click += new EventHandler(delegate (Object o, EventArgs e)
            {
                string[] data = new List<string>
                {
                    binding.txtName.Text,
                    binding.dtpNS.Value.ToString("dd/MM/yyyy"),
                    binding.txtMail.Text,
                    binding.txtPhone.Text,
                    binding.cboEthnic.Text ?? "",
                    binding.listBoxCity.SelectedItem == null ? binding.listBoxCity.Items[0].ToString() : binding.listBoxCity.SelectedItem.ToString()
                }.ToArray();
                string res = "";
                for (int i = 0; i < data.Length; i++)
                {
                    if (String.IsNullOrEmpty(data[i]))
                    {
                        DialogUtil.Instance.messageError("Không được bỏ trống thông tin !", "Lỗi");
                        return;
                    }
                    res += i == 0 ? data[i] : ", " + data[i];
                }
                writeFileTxt(new string[1] { res }, PATH + @"\Files\save.txt", true);
                DialogUtil.Instance.messageInfo("Lưu thành công", "Thông báo");
                clearInput(frm);
            });
        }
        public void clearInput(Form frm)
        {
            foreach (Control c in frm.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    c.Text = string.Empty;
                }
            }
        }
    }
}
