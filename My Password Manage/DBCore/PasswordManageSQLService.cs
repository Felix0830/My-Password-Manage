using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;

namespace PasswordManage.DAL
{
    public class PasswordManageSQLService
    {
        private static PasswordManageSQLService instance = null;
        private PasswordManageSQLService() { }

        public static PasswordManageSQLService Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new PasswordManageSQLService();
                }
                return instance;
            }
        }


        #region 添加站点类型
        public bool AddSiteType(string typeName)
        {
           
            string querySql = "SELECT TypeName FROM WebSiteType WHERE TypeName=@TypeName";
            string addSql = "INSERT INTO WebSiteType(TypeName)VALUES(@TypeName)";

            SQLiteParameter[] arParam = new SQLiteParameter[1];
            arParam[0] = new SQLiteParameter("@TypeName", typeName);

            object obj = DBHelper.ExcuteScalar(querySql, arParam);
            if (obj != null) return false;

            bool isSuccess = DBHelper.ExcuteNonQuery(addSql, arParam);

            return isSuccess;
        }
        #endregion

        #region 添加站点
        #endregion

        #region 获取所有站点
        #endregion

        #region 根据站点类型获得全部站点
        #endregion
    }
}
