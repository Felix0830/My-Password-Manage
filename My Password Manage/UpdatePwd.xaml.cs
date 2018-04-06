using My_Password_Manage.Model;
using PasswordManage.Common;
using PasswordManage.DAL;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for UpdatePwd.xaml
    /// </summary>
    public partial class UpdatePwd : Window
    {
        private PasswordManage.Model.PasswordModel passwordModel;

        public UpdatePwd()
        {
            InitializeComponent();
            ComboxDataBaind();
        }

        public UpdatePwd(PasswordManage.Model.PasswordModel p)
        {
            InitializeComponent();
            this.passwordModel = p;
            ComboxDataBaind();
        }

        private void ComboxDataBaind()
        {
            DataTable dt = PasswordManageSQLService.Instance.GetSiteType();

            List<SiteTypeModel> siteTypeLists = new List<SiteTypeModel>() { 
                new SiteTypeModel(){
                    TypeID=0,
                    TypeName="请选择"
                }
            };

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SiteTypeModel obj = new SiteTypeModel();
                obj.TypeID = int.Parse(dt.Rows[i][0].ToString());
                obj.TypeName = dt.Rows[i][1].ToString();

                siteTypeLists.Add(obj);
            }

            this.cbSiteType.ItemsSource = siteTypeLists;// dt.AsDataView();
            this.cbSiteType.SelectedValuePath = "TypeID";
            this.cbSiteType.DisplayMemberPath = "TypeName";

            var selectIndex = 0;
            foreach (var item in siteTypeLists)
            {
                if (item.TypeID != passwordModel.TypeID)
                    selectIndex++;
                else
                    break;
            }

            this.cbSiteType.SelectedIndex = selectIndex;

            txtUserName.Text = passwordModel.UserName;
            txtSite.Text = passwordModel.URL;
            txtUserRemarks.Text = passwordModel.Explain;
            txtPasswordKey.Password = passwordModel.Secretkey;            
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {  
            if(string.IsNullOrEmpty(txtUserOldPwd.Password.Trim()))
            {
                MessageBox.Show("请输入旧密码！");
                return;
            }

            if (string.IsNullOrEmpty(txtUserPwd.Password.Trim()))
            {
                MessageBox.Show("请输入新密码！");
                return;
            }

            if (string.IsNullOrEmpty(txtUserPwdConfirm.Password.Trim()))
            {
                MessageBox.Show("请输入确认新密码！");
                return;
            }

            if(txtUserPwd.Password.Trim() != txtUserPwdConfirm.Password.Trim())
            {
                MessageBox.Show("两次输入的新密码不一至");
                return;
            }

            try
            {
                EncryptHelper eh = new EncryptHelper(txtPasswordKey.Password.Trim());
                var oldpwdencryt = eh.EncryptString(txtUserOldPwd.Password.Trim());
                bool isSuccess = PasswordManageSQLService.Instance.VerificationOldPwd(passwordModel.Id, oldpwdencryt);
                if (!isSuccess)
                {
                    MessageBox.Show("输入的旧密码不正确，请检查！");
                    return;
                }

                var newpwdencryt = eh.EncryptString(txtUserPwd.Password.Trim());
                isSuccess = PasswordManageSQLService.Instance.UpdateSidePwd(passwordModel.Id, oldpwdencryt, newpwdencryt, txtUserRemarks.Text.Trim());
                if (!isSuccess)
                {
                    MessageBox.Show("更新新密码失败！");
                    return;
                }

                MessageBox.Show("新密码更新成功！");
            }
            catch(Exception ex)
            {
                //todo 记录日志
            }
        }
    }
}
