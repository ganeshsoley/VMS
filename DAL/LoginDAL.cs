using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LoginDAL
    {
        public static DataTable GetCompFinYear()
        {
            string strQry;
            DataTable dTable = new DataTable();

            try
            {
                strQry = "Select cf.dbid, Plant, YEAR(fromdate) fromdt, " +
                    " YEAR(todate) Todt " +
                    " FROM CompFinYr cf, SysCompany c " +
                    " WHERE cf.compDBID = c.CompanyID " +
                    " order by cf.dbid desc ";

                using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
                {
                    using (SqlCommand objCmd = new SqlCommand())
                    {
                        objCmd.Connection = Conn;
                        objCmd.CommandType = CommandType.Text;
                        objCmd.CommandText = strQry;

                        using (SqlDataAdapter da = new SqlDataAdapter(objCmd))
                        {
                            da.Fill(dTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dTable;
        }

        public static Int32 CheckLogin(string strUserName, string strPwd)
        {
            string strQry;
            Int32 UserID = 0;
            try
            {
                strQry = "SELECT DBID" +
                    " FROM USERPROFILE " +
                    " WHERE ISACTIVE = 1 " +
                    " AND LOGINNAME = @Login " +
                    " AND PASSWORD = @Pwd ";

                using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
                {
                    using (SqlCommand objCmd = new SqlCommand())
                    {
                        objCmd.Connection = Conn;
                        objCmd.CommandType = CommandType.Text;
                        objCmd.CommandText = strQry;
                        objCmd.Parameters.AddWithValue("@Login", strUserName);
                        objCmd.Parameters.AddWithValue("@Pwd", strPwd);

                        if (Conn.State != ConnectionState.Open)
                        {
                            Conn.Open();
                        }

                        object objTemp = objCmd.ExecuteScalar();
                        if (objTemp != null || objTemp != DBNull.Value)
                        {
                            UserID = Convert.ToInt32(objTemp);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return UserID;
        }
    }
}
