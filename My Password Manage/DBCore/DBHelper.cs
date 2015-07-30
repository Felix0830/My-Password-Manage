using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace PasswordManage.DAL
{
    class DBHelper
    {
        #region CreatedConnString
        private static readonly string connStr = @"E:\Pwd.db";
        private static SQLiteConnection CreateConn()
        {
            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
            sb.DataSource = connStr;

            SQLiteConnection conn = new SQLiteConnection();
            conn.ConnectionString = sb.ToString();

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        } 
        #endregion

        #region AddParameter
        private static void AddParameters(SQLiteCommand cmd, SQLiteParameter[] arParam)
        {
            if (cmd == null) throw new ArgumentNullException();
            if (arParam == null) throw new ArgumentNullException();

            foreach (var p in arParam)
            {
                if (p == null) continue;
                if (p.Value == null) p.Value = DBNull.Value;

                cmd.Parameters.Add(p);
            }

        } 
        #endregion

        #region Excute
        public static bool ExcuteNonQuery(string sql, SQLiteParameter[] arParam = null)
        {
            bool blReturnValue = false;
            using (SQLiteConnection conn = CreateConn())
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                if (arParam != null)
                {
                    AddParameters(cmd, arParam);
                }

                cmd.ExecuteNonQuery();
                blReturnValue = true;
            }
            return blReturnValue;
        }

        //public static bool ExcuteScalar(string sql , SQLiteParameter [] arParam=null)
        //{
        //    int returnValue;
        //    using (SQLiteConnection conn = CreateConn())
        //    {
        //        SQLiteCommand cmd = new SQLiteCommand(sql,conn);
        //        if (arParam != null)
        //        {
        //            AddParameters(cmd, arParam);
        //        }


        //       returnValue = int.Parse(cmd.ExecuteScalar().ToString());
        //    }
        //    return returnValue > 0;
        //}

        public static object ExcuteScalar(string sql, SQLiteParameter[] arParam=null)
        {
            object obj = null;
            using (SQLiteConnection conn = CreateConn())
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                if (arParam != null)
                {
                    AddParameters(cmd, arParam);
                }


                obj = cmd.ExecuteScalar();
            }
            return obj;
        }

        public static DataSet GetDataSetBySql(string sql, SQLiteParameter[] arParam = null)
        {
            DataSet ds = new DataSet();
            using (SQLiteConnection conn = CreateConn())
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                SQLiteDataAdapter da = new SQLiteDataAdapter();
                
                if (arParam != null)
                {
                    AddParameters(cmd, arParam);
                }
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            return ds;
        }

        public static DataTable GetDataTableBySql(string sql, SQLiteParameter[] arParam = null)
        {
            DataTable dt = new DataTable();
            using (SQLiteConnection conn = CreateConn())
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                SQLiteDataAdapter da = new SQLiteDataAdapter();

                if (arParam != null)
                {
                    AddParameters(cmd, arParam);
                }
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            return dt;
        }

        public static SQLiteDataReader GetDataReaderBySql(string sql, SQLiteParameter[] arParam = null)
        {
            SQLiteDataReader dr = null;
            using (SQLiteConnection conn = CreateConn())
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                if (arParam != null)
                {
                    AddParameters(cmd, arParam);
                }

                dr = cmd.ExecuteReader();
            }
            return dr;
        }
        #endregion

        


    }
}
