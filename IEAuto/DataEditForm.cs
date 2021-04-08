using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IEAuto
{
    public partial class DataEditForm : Form
    {
        string[] data;
        string password;
        public DataEditForm(string[] data,string password)
        {
            InitializeComponent();
            this.data = data;
            this.password = password;
            for(int i = 0; i < data.Length; ++i)
            {
                editTextBox.Text += data[i] + "\n";
            }
        }

        private void DataEditForm_Load(object sender, EventArgs e)
        {

        }

        private void editButton_Click(object sender, EventArgs e)
        {
            string[] new_data = editTextBox.Text.Split('\n');
            string encrytoString = "";
            for(int i = 0;i< new_data.Length; ++i)
            {
                encrytoString += new_data[i] + "\n";
            }
            MessageBox.Show(String.Format("암호화할 데이터 : {0}", encrytoString));
            byte[] res = Secret.AesEncrypt(Encoding.ASCII.GetBytes(encrytoString), password);
            var stream = System.IO.File.OpenWrite(Application.StartupPath + "\\sjData.dat");
            
            stream.Write(Encoding.ASCII.GetBytes(Secret.SHA256Hash(password)), 0, Encoding.ASCII.GetBytes(Secret.SHA256Hash(password)).Length);
            stream.Write(res, 0, res.Length);
            stream.Close();
            MessageBox.Show("저장하였으며 프로그램을 종료합니다.");
            Application.Exit();
        }

        private void cancleButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
