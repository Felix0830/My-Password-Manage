using PasswordManage.DAL;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace My_Password_Manage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnGeneratePwd_Click(object sender, RoutedEventArgs e)
        {
            GeneratePwd fmgp = new GeneratePwd();
            fmgp.ShowDialog(); 
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string typeName = txtSiteType.Text.Trim();
            if (typeName == string.Empty)
            {
                MessageBox.Show("站点类型不允许为空！");
                return;
            }

            bool isSuccess = PasswordManageSQLService.Instance.AddSiteType(typeName);
            if (isSuccess)
            {
                MessageBox.Show("站点类型添加成功！");
            }
            else
            {
                MessageBox.Show("站点类型添加失败！");
            }
        }
    }
}
