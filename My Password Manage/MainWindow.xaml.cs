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
using System.Data;
using PasswordManage.Common;

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
            ComboxDataBaind();
            InitGridView();
        }

        private void InitGridView()
        {
            DataTable dt = PasswordManageSQLService.Instance.GetPwdInfos();
            dgPwdInfoView.ItemsSource = dt.AsDataView();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ComboxDataBaind()
        {
            
            DataTable dt = PasswordManageSQLService.Instance.GetSiteType();
            this.cbChooseType.ItemsSource = dt.AsDataView();
            this.cbChooseType.SelectedValuePath = "TypeID";
            this.cbChooseType.DisplayMemberPath = "TypeName";

            this.cbSiteType.ItemsSource = dt.AsDataView();
            this.cbSiteType.SelectedValuePath = "TypeID";
            this.cbSiteType.DisplayMemberPath = "TypeName";
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
                ComboxDataBaind();
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
            string siteTypeID = cbSiteType.SelectedValue.ToString();
            string url = txtSite.Text;
            string userName = txtUserName.Text;
            if (txtUserPwd.Password != txtUserPwdConfirm.Password)
            {
                MessageBox.Show("两次密码输入一致，请重新输入！");
                txtUserPwd.Password = string.Empty;
                txtUserPwdConfirm.Password = string.Empty;
                return;
            }
            string pwd = txtUserPwd.Password;
            string key = txtPasswordKey.Text.Trim();
            if (string.IsNullOrEmpty(key))
            {
                MessageBox.Show("请输入加密密钥！");
                return;
            }
            string explain = txtUserRemarks.Text;

            try
            {
                EncryptHelper eh = new EncryptHelper(key);
                PasswordModel model = new PasswordModel()
                {
                    TypeID = int.Parse(siteTypeID),
                    URL = url,
                    UserName = userName,
                    UserPWD = eh.EncryptString(pwd),
                    Explain = explain,
                    UpdateTime = DateTime.Now
                };

                bool isSuccess = PasswordManageSQLService.Instance.AddSite(model);
                if (isSuccess)
                {
                    MessageBox.Show("新网站添加成功！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("系统发生异常，请稍候再试！");
            }
            finally
            {
                cbSiteType.SelectedIndex = 0;
                txtSite.Text = string.Empty;
                txtUserName.Text = string.Empty;
                txtUserPwd.Password = string.Empty;
                txtUserPwdConfirm.Password = string.Empty;
                txtPasswordKey.Text = string.Empty;
                txtUserRemarks.Text = string.Empty;
            }
        }


    }
}
