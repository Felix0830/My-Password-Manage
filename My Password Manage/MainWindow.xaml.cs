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
using My_Password_Manage.Model;

namespace My_Password_Manage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static DataTable _dt = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgPwdInfoView.AutoGenerateColumns = false;
            ComboxDataBaind();
            InitGridView();
        }

        private void InitGridView()
        {
            GetPwdInfo();
            BindGrid();
        }

        private void BindGrid()
        {
            dgPwdInfoView.ItemsSource = _dt.AsDataView();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
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

            this.cbChooseType.ItemsSource = siteTypeLists;// dt.AsDataView();           
            this.cbChooseType.SelectedValuePath = "TypeID";
            this.cbChooseType.DisplayMemberPath = "TypeName";
            this.cbChooseType.SelectedIndex = 0;

            this.cbSiteType.ItemsSource = siteTypeLists;// dt.AsDataView();
            this.cbSiteType.SelectedValuePath = "TypeID";
            this.cbSiteType.DisplayMemberPath = "TypeName";
            this.cbSiteType.SelectedIndex = 0;
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
            var siteTypeObj = (SiteTypeModel)cbSiteType.SelectedItem;
            string siteTypeID = siteTypeObj.TypeID.ToString();
            if (siteTypeObj.TypeName == "请选择")
            {
                MessageBox.Show("请选择网站类型");
                return;
            }
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
                    //重新加载gridview
                    InitGridView();
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

        private void cbChooseType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataTable dt = null;
            var item = (SiteTypeModel)cbChooseType.SelectedItem;
            if (item == null)
            {
                return;
            }

            if (item.TypeName == "请选择")
            {
                dt = PasswordManageSQLService.Instance.GetPwdInfos();
            }
            else
            {
                dt = PasswordManageSQLService.Instance.GetPwdInfos(item.TypeID);
            }

            dgPwdInfoView.ItemsSource = dt.AsDataView();
        }

        /// <summary>
        /// 查看密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowPwd_Click(object sender, RoutedEventArgs e)
        {
            string key = txtPwdKey.Password;
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            DataTable dt = null;
            var item = (SiteTypeModel)cbChooseType.SelectedItem;
            if (item.TypeName == "请选择")
            {
                //dt = PasswordManageSQLService.Instance.GetPwdInfos();
                dt = GetPwdInfo();
            }
            else
            {
                //dt = PasswordManageSQLService.Instance.GetPwdInfos(item.TypeID);
                dt = GetPwdInfo(item.TypeID);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                EncryptHelper encryptHelper = new EncryptHelper(key);
                string v = encryptHelper.DecryptString(dt.Rows[i][4].ToString());
                dt.Rows[i][4] = v;
            }

            BindGrid();
        }

        private DataTable GetPwdInfo(int? typeId=null)
        {
            if (typeId.HasValue)
            {
                _dt = PasswordManageSQLService.Instance.GetPwdInfos(typeId.Value);
            }
            else
            {
                _dt = PasswordManageSQLService.Instance.GetPwdInfos();
            }
            return _dt;
        }

        private void tabManWin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO: 未解决中。。。。
            //MessageBox.Show("qq");
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            var id = int.Parse(bt.Tag.ToString());

            var p = new PasswordModel();
            for (int i = 0; i<_dt.Rows.Count;i++ )
            {
                var cuid = int.Parse(_dt.Rows[i]["Id"].ToString());
                if(cuid == id)
                {
                   p.UserName = _dt.Rows[i]["Uid"].ToString();
                   p.URL =_dt.Rows[i]["Url"].ToString();
                   p.TypeID = int.Parse(_dt.Rows[i]["TypeID"].ToString());
                   p.Explain = _dt.Rows[i]["Explain"].ToString();
                   p.Id = id;
                   p.Secretkey = "iloveyou";
                }
            }
            UpdatePwd updatePwd = new UpdatePwd(p);
            updatePwd.ShowDialog();
        }
    }
}
