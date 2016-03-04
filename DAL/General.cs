using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EntityObject;

namespace DAL
{
    public class General
    {
        const string gstrDBPassword = "";
        const string gstrDataSource = "ORA";
        const string gstrDBUser = "";


        const string MstrDBUser = "SYSTEM";
        const string MstrDBPassword = "MANAGER";
        const string MstrDataSource = "ORA";

        static public string GetSQLConnectionString()
        {
            // return "Data Source=Administrator\\Database Engine;User ID=ITPROJECT\\Administrator;Password = dtpl@admin1;Initial Catalog=DTPL;Min Pool Size=5;Max Pool Size=100;Trusted_Connection=True";   //;Connect Timeout=30
            //return "Data Source=IT5.DTPLF.COM\\SQLEXPRESS;Initial Catalog=DTPL;User ID=DTPL;Password =Dtpl@1234;Min Pool Size=5;Max Pool Size=100;Integrated Security = True;";   //;Connect Timeout=30
            //return "Data Source=ITPROJECT\\SQLEXPRESS,1433;Initial Catalog=DTPL;User ID=VMSUser;Password=Dtpl@vms16; Min Pool Size = 5; Max Pool Size = 100";

            string strDataSource = "Data Source=" + GetSqlServerPCName() + "\\" + GetSqlServerInstanceName() + "," + GetSQLPort() + ";";
            string strDBName = "Initial Catalog =" + GetDatabaseName() + ";";
            string strUser = "User ID=" + GetDBUserName() + ";";
            string strPwd = "Password=" + GetDBPwd() + ";";
            return strDataSource + strDBName + strUser + strPwd + "Min Pool Size = 5; Max Pool Size = 100";
        }

        public static string GetSqlServerPCName()
        {
            //return "ITPROJECT";
            return "GATE-PC";
        }

        public static int GetSQLPort()
        {
            return 1433;
        }
            
        public static string GetSqlServerInstanceName()
        {
            return "SQLEXPRESS";
        }

        public static string GetDatabaseName()
        {
            return "DTPL";
        }

        public static string GetDBUserName()
        {
            return "VMSUser";
        }

        public static string GetDBPwd()
        {
            return "Dtpl@vms16";
        }

        public static string GetMachineName()
        {
            return Environment.MachineName;
        }

        static public string LoadCompany(long mMastID, string mUserName)
        {

            UserCompany CurrentCompany = new UserCompany();
            CurrentCompany.m_UserName = mUserName;
            CurrentCompany.m_DataSource = MstrDataSource;
            string ConnStr = GetSQLConnectionString();
            SqlConnection conn = new SqlConnection(ConnStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            //cmd.CommandText = "SELECT A.*, B.FromDate FROMDATE, B.ToDate TODATE, " +
            //    " B.DBID MainDBID, B.TRANSACTIONLOCK TRANSACTIONLOCK, SysDate " +
            //    " FROM SYSTEM.SysCompany A, dtplsh1415.CompFinYr B " +
            //    " WHERE A.CompanyID = B.compdbid " +
            //    " AND B.DBID = " + mMastID;

            cmd.CommandText = "SELECT A.*, B.FromDate FROMDATE, B.ToDate TODATE, " +
                " B.DBID MainDBID, B.TRANSACTIONLOCK TRANSACTIONLOCK, GETDATE() as SysDate " +
                " FROM SysCompany A, CompFinYr B " +
                " WHERE A.CompanyID = B.compdbid " +
                " AND B.DBID = " + mMastID;

            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            try
            {
                //a = dr["LoginName"].ToString();
                CurrentCompany.m_FromDate = Convert.ToDateTime(dr["FromDate"]);  //.ToString("dd-MMM-yy")
                CurrentCompany.m_ToDate = Convert.ToDateTime(dr["ToDate"]);  //.ToString("dd-MMM-yy")
                CurrentCompany.m_Dbid = Convert.ToInt32(dr["COMPANYID"]);
                CurrentCompany.m_Copdbid = Convert.ToInt32(dr["COMPANYID"]);
                CurrentCompany.m_MainDbid = Convert.ToInt32(dr["MainDBID"]);
                CurrentCompany.m_Company = dr["Name"].ToString();
                CurrentCompany.m_Address1 = dr["Add1"].ToString();
                CurrentCompany.m_Address2 = dr["Add2"].ToString();
                CurrentCompany.m_City = dr["City"].ToString();
                CurrentCompany.m_PlantID = dr["Plant"].ToString();
                CurrentCompany.m_Phone1 = dr["Phone"].ToString();
                CurrentCompany.m_ECCNO = dr["ECCNO"].ToString();
                CurrentCompany.m_Range = dr["RANGE"].ToString();
                CurrentCompany.m_Office_Phone = dr["OFFICE_PHONE"].ToString();
                CurrentCompany.m_Office_Fax = dr["OFFICE_FAX"].ToString();
                CurrentCompany.m_CSTNO = dr["CSTNO"].ToString();
                CurrentCompany.M_BSTNO = dr["BSTNO"].ToString();
                //SysDate = dr["SysDate"].ToString(); //Format(.Fields("SysDate"), "dd/mm/yyyy") 
            }
            catch
            {
                // MessageBox.Show("Invalid UserName..");
            }
            return "ok";
            
        }

        static public UserCompany LoadCompany(long mMastID)
        {
            UserCompany CurrentCompany = new UserCompany();
            //CurrentCompany.m_UserName = mUserName;
            //CurrentCompany.m_DataSource = MstrDataSource;
            string ConnStr = GetSQLConnectionString();
            SqlConnection conn = new SqlConnection(ConnStr);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            //cmd.CommandText = "SELECT A.*, B.FromDate FROMDATE, B.ToDate TODATE, " +
            //    " B.DBID MainDBID, B.TRANSACTIONLOCK TRANSACTIONLOCK, SysDate " +
            //    " FROM SYSTEM.SysCompany A, dtplsh1415.CompFinYr B " +
            //    " WHERE A.CompanyID = B.compdbid " +
            //    " AND B.DBID = " + mMastID;

            cmd.CommandText = "SELECT A.*, B.FromDate FROMDATE, B.ToDate TODATE, " +
                " B.DBID MainDBID, B.TRANSACTIONLOCK TRANSACTIONLOCK, GETDATE() as SysDate " +
                " FROM SysCompany A, CompFinYr B " +
                " WHERE A.CompanyID = B.compdbid " +
                " AND B.DBID = @MastID ";       // + mMastID;

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MastID", mMastID);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            try
            {
                //a = dr["LoginName"].ToString();
                CurrentCompany.m_FromDate = Convert.ToDateTime(dr["FromDate"]);  //.ToString("dd-MMM-yy")
                CurrentCompany.m_ToDate = Convert.ToDateTime(dr["ToDate"]);  //.ToString("dd-MMM-yy")
                CurrentCompany.m_Dbid = Convert.ToInt32(dr["COMPANYID"]);
                CurrentCompany.m_Copdbid = Convert.ToInt32(dr["COMPANYID"]);
                CurrentCompany.m_MainDbid = Convert.ToInt32(dr["MainDBID"]);
                CurrentCompany.m_Company = dr["Name"].ToString();
                CurrentCompany.m_Address1 = dr["Add1"].ToString();
                CurrentCompany.m_Address2 = dr["Add2"].ToString();
                CurrentCompany.m_City = dr["City"].ToString();
                CurrentCompany.m_PlantID = dr["Plant"].ToString();
                CurrentCompany.m_Phone1 = dr["Phone"].ToString();
                CurrentCompany.m_ECCNO = dr["ECCNO"].ToString();
                CurrentCompany.m_Range = dr["RANGE"].ToString();
                CurrentCompany.m_Office_Phone = dr["OFFICE_PHONE"].ToString();
                CurrentCompany.m_Office_Fax = dr["OFFICE_FAX"].ToString();
                CurrentCompany.m_CSTNO = dr["CSTNO"].ToString();
                CurrentCompany.M_BSTNO = dr["BSTNO"].ToString();
                //SysDate = dr["SysDate"].ToString(); //Format(.Fields("SysDate"), "dd/mm/yyyy") 
            }
            catch
            {
                // MessageBox.Show("Invalid UserName..");
            }
            return CurrentCompany;
        }

        /// <summary>
        /// Created By Ganesh To Generalize the process of Generating Table DBID.
        /// </summary>
        /// <param name="Conn">Connection Object</param>
        /// <param name="TabName">Table Name for which DBID is to be generated.</param>
        /// <returns>Integer Unique Value for Table.</returns>
        public static int GenerateDBID(SqlConnection Conn, string TabName)
        {
            int id;
            using (SqlCommand objCmd = new SqlCommand())
            {
                try
                {
                    objCmd.Connection = Conn;
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "select max(dbid) id from " + TabName;
                    //objCmd.Transaction = objTrans;

                    if (Conn.State != ConnectionState.Open)
                    {
                        Conn.Open();
                    }

                    object objTemp = objCmd.ExecuteScalar();
                    if (objTemp == null || objTemp == DBNull.Value)
                    {
                        id = 1;
                    }
                    else
                    {
                        id = Convert.ToInt32(objTemp);
                        id += 1;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return id;
        }

        /// <summary>
        /// Returns Unique value generated from Sequence.
        /// </summary>
        /// <param name="SeqName">Name of Sequence to Generate Unique ID.</param>
        /// <param name="Conn">Connection Object.</param>
        /// <returns></returns>
        public static int GenerateDBID(string SeqName, SqlConnection Conn)
        {
            int id;
            using (SqlCommand objCmd = new SqlCommand())
            {
                try
                {
                    objCmd.Connection = Conn;
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "SELECT NEXT VALUE FOR " + SeqName;

                    if (Conn.State != ConnectionState.Open)
                    {
                        Conn.Open();
                    }

                    object objTemp = objCmd.ExecuteScalar();
                    if (objTemp == null || objTemp == DBNull.Value)
                    {
                        id = 1;
                    }
                    else
                    {
                        id = Convert.ToInt32(objTemp);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return id;
        }
    }
}
