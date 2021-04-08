using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IEAuto
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new LoginForm(Application.StartupPath));
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show("프로그램을 종료합니다.");
                Application.Exit();
            }
        }
    }
}
