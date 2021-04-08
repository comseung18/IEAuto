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
    public partial class MainForm : Form
    {
        string[] data;
        string password;
        public MainForm(string[] data,string password)
        {
            InitializeComponent();

            this.data = data;
            this.password = password;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("폼을 종료합니다.");
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataEditForm dataEditForm = new DataEditForm(data,password);
            dataEditForm.ShowDialog();
        }
    }
}
