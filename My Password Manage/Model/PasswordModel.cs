using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManage.Model
{
    public class PasswordModel
    {
        /// <summary>
        /// 主键 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 网站地址
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// 网站类型ID
        /// </summary>
        public int TypeID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string UserPWD { get; set; }
        /// <summary>
        /// 使用过的密码
        /// </summary>
        public string OldPWD { get; set; }
        /// <summary>
        /// 其他说明
        /// </summary>
        public string Explain { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string Secretkey { get; set; }
    }
}
