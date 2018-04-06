using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;
using PasswordManage.Model;
using System.Data;

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

        public DataTable GetSiteType()
        {
            string sql = "SELECT TypeID,TypeName FROM WebSiteType";
            return DBHelper.GetDataTableBySql(sql);
        }

        #region 添加站点
        public bool AddSite(PasswordModel model)
        {
            string addSql = @"INSERT INTO PasswordList(TypeID,Url,Uid,Pwd,Explain,UpdatedTime)
                            VALUES(@typeid,@url,@uid,@pwd,@explain,@updatedtime)";

            SQLiteParameter[] arParam = new SQLiteParameter[6];
            arParam[0] = new SQLiteParameter("@typeid", model.TypeID);
            arParam[1] = new SQLiteParameter("@url", model.URL);
            arParam[2] = new SQLiteParameter("@uid", model.UserName);
            arParam[3] = new SQLiteParameter("@pwd", model.UserPWD);
            arParam[4] = new SQLiteParameter("@explain", model.Explain);
            arParam[5] = new SQLiteParameter("@updatedtime", model.UpdateTime);

            bool isSuccess = DBHelper.ExcuteNonQuery(addSql, arParam);
            return isSuccess;
        }
        #endregion

        #region 获取所有站点
        public DataTable GetPwdInfos(int typeID = 0)
        {
            SQLiteParameter[] arParam = null;
            string querySql = "SELECT Id,TypeID,Url,Uid,Pwd,Explain FROM PasswordList ";
            if (typeID != 0)
            {
                querySql += " WHERE TypeID=@typeid";
                arParam = new SQLiteParameter[1];
                arParam[0] = new SQLiteParameter("@typeid",typeID);
            }
            return DBHelper.GetDataTableBySql(querySql, arParam);
        }
        #endregion

        #region 修改站点密码

        /// <summary>
        /// 验证旧密码是否正确
        /// </summary>
        /// <param name="sideId">主键</param>
        /// <param name="oldPwd">旧密码</param>
        /// <returns></returns>
        public bool VerificationOldPwd(int sideId, string oldPwd)
        {
            var sql = "select id from PasswordList where id=@id and pwd=@pwd";
            SQLiteParameter[] param = new SQLiteParameter[2];
            param[0] = new SQLiteParameter("@id", sideId);
            param[1] = new SQLiteParameter("@pwd", oldPwd);
            var obj = DBHelper.ExcuteScalar(sql, param);
            return obj != null;
        }

        public bool UpdateSidePwd(int sideId,string oldPwd, string newPwd,string explain)
        {
            var sql = "update PasswordList set oldpwd=@oldPwd || '$!$' || oldpwd,pwd=@newpwd,explain=@explain,updatedTime=datetime('now', 'localtime') where id=@id";
            SQLiteParameter[] param = new SQLiteParameter[4];
            param[0] = new SQLiteParameter("@id", sideId);
            param[1] = new SQLiteParameter("@newpwd", newPwd);
            param[2] = new SQLiteParameter("@oldpwd", oldPwd);
            param[3] = new SQLiteParameter("@explain",explain);
            var result = DBHelper.ExcuteNonQuery(sql, param);
            return result;
        }

        #endregion
    }
}
