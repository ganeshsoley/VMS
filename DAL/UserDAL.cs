using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EntityObject;

namespace DAL
{
    public class UserDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static UserList GetList(string strSelectQry)
        {
            UserList objList = null;
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    objCmd.Connection = Conn;
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = strSelectQry;

                    if (Conn.State != ConnectionState.Open)
                    {
                        Conn.Open();
                    }
                    SqlDataReader objReader = objCmd.ExecuteReader();
                    if (objReader.HasRows)
                    {
                        objList = new UserList();
                        while (objReader.Read())
                        {
                            objList.Add(FillDataRecord4List(objReader));
                        }
                    }
                    objReader.Close();
                    objReader.Dispose();
                }
            }
            return objList;
        }

        public static User GetItem(int dbID)
        {
            User ObjUser = null;
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    objCmd.Connection = Conn;
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "Select * From UserProfile " +
                        "Where dbid = @dbid";
                    objCmd.Parameters.AddWithValue("@dbid", dbID);

                    if (Conn.State != ConnectionState.Open)
                    {
                        Conn.Open();
                    }

                    SqlDataReader objReader = objCmd.ExecuteReader();
                    if (objReader.Read())
                    {
                        ObjUser = FillDataRecord(objReader);
                        ObjUser.IsNew = false;
                    }
                    objReader.Close();
                    objReader.Dispose();
                }
            }
            return ObjUser;
        }

        public static bool Save(User objUser)
        {
            int result = 0;
            UserCompany CurrentCompany = new UserCompany();

            string strSaveQry, strEncryptPwd;

            if (objUser.IsNew)
            {
                strSaveQry = "Insert into UserProfile (DBID, FIRSTNAME, LOGINNAME, PASSWORD, ROLE, " +
                    " BRANCHCODE, COMPANYCODE, ST_DATE, MODIFY_DATE, DEPT, ISACTIVE, DEPTHEAD, CRBY, MODBY, MACHINENAME) " +
                    " VALUES ( @dbid, @FirstName, @LoginName, @Password, @Role, " +
                    " @BranchCode, @CompanyCode, @StDate, @ModifyDate, @Dept, @IsActive, @DeptHead, " +
                    " @CrBy, @ModBy, @MachineName)";
            }
            else
            {
                strSaveQry = "UPDATE UserProfile " +
                    "set FIRSTNAME = @FirstName, LOGINNAME = @LoginName, PASSWORD = @Password, " +
                    " ROLE = @Role, BRANCHCODE = @BranchCode, COMPANYCODE = @CompanyCode, " +
                    " MODIFY_DATE = @ModifyDate, DEPT = @Dept, ISACTIVE = @IsActive, DEPTHEAD = @DeptHead, " +
                    " MODBY = @ModBy, MACHINENAME = @MachineName " +
                    " WHERE DBID = @dbid ";
            }

            SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString());
            SqlCommand objCmd = new SqlCommand();
            try
            {
                // Temporary disabled as stored procedure is not created.
                //strEncryptPwd = GetEncryptPwd(objUser.Password);
                strEncryptPwd = objUser.Password;

                objCmd.Connection = Conn;
                objCmd.CommandType = CommandType .Text;
                objCmd.CommandText  = strSaveQry;

                objCmd.Parameters.AddWithValue("@dbid", objUser.DBID);
                objCmd.Parameters.AddWithValue("@FirstName", objUser.FirstName);
                objCmd.Parameters.AddWithValue("@LoginName", objUser.LoginName);
                objCmd.Parameters.AddWithValue("@Password", strEncryptPwd);
                objCmd.Parameters.AddWithValue("@Role", objUser.Role);
                objCmd.Parameters.AddWithValue("@BranchCode", objUser.BranchCode);
                objCmd.Parameters.AddWithValue("@CompanyCode", objUser.CompanyCode);
                if (objUser.IsNew)
                {
                    objCmd.Parameters.AddWithValue("@StDate", DateTime.Now);
                    objCmd.Parameters.AddWithValue("@CrBy", CurrentCompany.m_UserName);
                    objUser.DBID = GenerateDBID();
                }
                objCmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                objCmd.Parameters.AddWithValue("@ModBy", CurrentCompany.m_UserName);
                objCmd.Parameters.AddWithValue("@MachineName", General.GetMachineName());

                objCmd.Parameters.AddWithValue("@Dept", objUser.Dept);
                objCmd.Parameters.AddWithValue("@IsActive", objUser.IsActive);
                objCmd.Parameters.AddWithValue("@DeptHead", objUser.DeptHead);

                if(Conn.State != ConnectionState .Open)
                {
                    Conn.Open();
                }
                result  = objCmd.ExecuteNonQuery();
                objCmd.Dispose();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }

            return (result > 0);
        }

        /// <summary>
        /// Deletes Specified Record from Database. 
        /// </summary>
        /// <param name="dbID">Unique Value by which record is identified in Table.</param>
        /// <returns>Boolean Value True when record is deleted successfully
        /// otherwise returns False.</returns>
        public static bool Delete(int dbID)
        {
            int result = 0;
            using (SqlConnection Conn = new SqlConnection())
            {
                SqlCommand objCmd = new SqlCommand();
                objCmd.Connection = Conn;
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = "Delete from UserProfile " +
                    " Where dbid = @dbid";
                objCmd.Parameters.AddWithValue("@dbid", dbID);

                if (Conn.State != ConnectionState.Open)
                {
                    Conn.Open();
                }
                result = objCmd.ExecuteNonQuery();
                objCmd.Dispose();
            }
            return (result > 0);
        }

        private static User FillDataRecord(IDataReader myDataRec)
        {
            User objUser = new User();
            objUser.DBID = Convert.ToInt32(myDataRec["dbid"]);
            objUser.FirstName = Convert.ToString(myDataRec["firstname"]);
            objUser.LoginName = Convert.ToString(myDataRec["LoginName"]);
            //objUser.Password = GetDecryptPwd(Convert.ToString(myDataRec["Password"]));
            objUser.Password = Convert.ToString(myDataRec["Password"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("Role")))
                objUser.Role = Convert.ToString(myDataRec["Role"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("BranchCode")))
                objUser.BranchCode = Convert.ToInt32(myDataRec["Branchcode"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("CompanyCode")))
                objUser.CompanyCode = Convert.ToInt32(myDataRec["CompanyCode"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("Dept")))
                objUser.Dept = Convert.ToString(myDataRec["Dept"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("IsActive")))
                objUser.IsActive = Convert.ToInt32(myDataRec["IsActive"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("depthead")))
                objUser.DeptHead = Convert.ToInt32(myDataRec["depthead"]);

            return objUser;
        }

        private static string GetDecryptPwd(string pwd)
        {
            string DecryptPwd = string.Empty;
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select decrypt('" + pwd + "') from dual";

                if (Conn.State != ConnectionState.Open)
                {
                    Conn.Open();
                }
                SqlDataReader oreader = cmd.ExecuteReader();
                if (oreader.HasRows)
                {
                    while (oreader.Read())
                    {
                        DecryptPwd = Convert.ToString(oreader[0]);
                    }
                }
                oreader.Close();
                cmd.Dispose();
            }
            return DecryptPwd;
        }

        private static string GetEncryptPwd(string pwd)
        {
            string EncryptPwd;
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "encryptproc";
                cmd.Parameters.Add("plain", SqlDbType.VarChar).Value = pwd;
                cmd.Parameters.Add("mLogName", SqlDbType.VarChar,15).Direction = ParameterDirection.Output;

                try
                {
                    if (Conn.State != ConnectionState.Open)
                    {
                        Conn.Open();
                    }
                    cmd.ExecuteNonQuery();
                    EncryptPwd = Convert.ToString(cmd.Parameters["mLogName"].Value);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                Conn.Close();
            }
            return EncryptPwd;
        }

        private static User FillDataRecord4List(IDataReader myDataRec)
        {
            User objUser = new User();
            objUser.DBID = Convert.ToInt32(myDataRec["dbid"]);
            objUser.LoginName = Convert.ToString(myDataRec["LoginName"]);
            objUser.Role = Convert.ToString(myDataRec["Role"]);
            return objUser;
        }

        private static int GenerateDBID()
        {
            int id = 0;
            SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString());
            SqlCommand objCmd = new SqlCommand();

            objCmd.Connection = Conn;
            objCmd.CommandType = CommandType.Text;
            objCmd.CommandText = "Select Max(DBID) ID from UserProfile ";

            try
            {
                if (Conn.State != ConnectionState.Open)
                {
                    Conn.Open();
                }

                SqlDataReader objReader = objCmd.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        id = Convert.ToInt32(objReader["id"]);
                        id += 1;
                    }
                }
                else
                {
                    id = 1;
                }
                objReader.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objCmd.Dispose();
                Conn.Close();
                Conn.Dispose();
            }
            return id;
        }
    }
}
