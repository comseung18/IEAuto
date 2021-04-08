using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace IEAuto
{
    public partial class LoginForm : Form
    {
        string hashPassword;
        string[] data;
        public LoginForm(string path)
        {
            InitializeComponent();

            // 데이터 파일을 읽음.
            data = System.IO.File.ReadAllLines(path + "\\sjData.dat");
            hashPassword = data[0];
        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }

        private void passwordButton_Click(object sender, EventArgs e)
        {
            if(Secret.SHA256Hash(passwordTextBox.Text) != hashPassword)
            {
                MessageBox.Show("옳지 않은 비밀번호 입니다.");
            }
            else
            {
                this.Hide();
                string subData ="";
                for(int i = 0; i < data.Length - 1; ++i)
                {
                    subData += data[i + 1] + "\n";
                }
                byte[] decrytoData = Secret.AesDecrypt(Encoding.ASCII.GetBytes(subData), passwordTextBox.Text);
                string decrytoString = Encoding.ASCII.GetString(decrytoData);
                MainForm mainForm = new MainForm(decrytoString.Split("\n"), passwordTextBox.Text);
                mainForm.Show();
            }
        }

        private void passwordTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                passwordButton.PerformClick();
            }
        }
    }
}
