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
using PasswordManage.Model;

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            MessageBox.Show("sfs");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// 添加网站类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            txtSiteType.Text = string.Empty;
        }

        /// <summary>
        /// 添加网站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddSite_Click(object sender, RoutedEventArgs e)
        {
            string siteType = cbSiteType.SelectedValue.ToString();
            string url = txtSite.Text;
            string userName = txtUserName.Text;
            if (txtUserPwd.Password != txtUserPwdConfirm.Password)
            {
                MessageBox.Show("两次密码输入一致，请重新输入！");
                return;
            }
            string pwd = txtUserPwd.Password;
            string explain = txtUserRemarks.Text;

            PasswordModel model = new PasswordModel()
            {
                TypeID = int.Parse(siteType),
                URL = url,
                UserName = userName,
                UserPWD = pwd,
                Explain = explain
            };

            bool isSuccess = PasswordManageSQLService.Instance.AddSite(model);

        }

       


    }
}
