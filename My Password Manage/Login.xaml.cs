using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace My_Password_Manage
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string userName = txtUserName.Text;
            string userPwd = txtPassword.Password;
            //TODO: Test
            userName = "zgshi";
            userPwd = "123456";

            if (userName == "zgshi" && userPwd == "123456")
            {
                MainWindow manwin = new MainWindow();
                manwin.Show();
                this.Hide();
                return;
            }

            MessageBox.Show("用户名或密码错误！");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
