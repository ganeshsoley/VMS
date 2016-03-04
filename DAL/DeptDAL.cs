using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EntityObject;

namespace DAL
{
    public class DeptDAL
    {
        #region Private Method(s)
        /// <summary>
        /// Fills values fetched from Database to Object objDept.
        /// </summary>
        /// <param name="myDataRec">Record Object containing data values.</param>
        /// <returns>Returns object ObjDept containing Data values from Database.</returns>
        private static Department FillDataRecord(IDataRecord myDataRec)
        {
            Department objDept = new Department();
            objDept.IsLoading = true;
            objDept.DBID = Convert.ToInt32(myDataRec["DBID"]);
            objDept.DeptName = Convert.ToString(myDataRec["DEPTNAME"]);
            objDept.Description = Convert.ToString(myDataRec["DESCRIPTION"]);
            objDept.IsActive = Convert.ToBoolean(myDataRec["ISACTIVE"]);

            objDept.IsNew = false;
            objDept.IsEdited = false;
            objDept.IsDeleted = false;
            objDept.IsLoading = false;

            return objDept;
        }
        #endregion

        /// <summary>
        /// This method retrieves "Department" Record, which is retrieved from Database.
        /// </summary>
        /// <param name="dbid">Unique ID value based on which Record will be fetched from Database.</param>
        /// <returns>Object "Department" containing Data Values.</returns>
        public static Department GetItem(int dbid)
        {
            Department objDept = null;
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    try
                    {
                        objCmd.Connection = Conn;
                        objCmd.CommandType = CommandType.Text;
                        objCmd.CommandText = "SELECT * FROM DEPTMAST " +
                            " WHERE DBID = @DBID";
                        objCmd.Parameters.AddWithValue("@DBID", dbid);

                        if (Conn.State != ConnectionState.Open)
                        {
                            Conn.Open();
                        }

                        SqlDataReader oReader = objCmd.ExecuteReader();
                        if (oReader.Read())
                        {
                            objDept = FillDataRecord(oReader);
                            objDept.IsNew = false;
                        }
                        oReader.Close();
                        oReader.Dispose();
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            return objDept;
        }

        /// <summary>
        /// This method provides List of Departments available in Database.
        /// </summary>
        /// <param name="strWhere">Specifies condition for retrieving records.</param>
        /// <returns>Collection of Department Objects.</returns>
        public static DepartmentList GetList(string strWhere)
        {
            DepartmentList objList = null;
            string strSql = "Select * from DEPTMAST ";

            if (strWhere != string.Empty)
                strSql = strSql + " WHERE " + strWhere;
            strSql += " ORDER BY DEPTNAME";

            using (SqlConnection Conn = new SqlConnection (General.GetSQLConnectionString()))
            {
                using (SqlCommand objCmd = new SqlCommand ())
                {
                    objCmd.Connection = Conn;
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = strSql;

                    if (Conn.State != ConnectionState.Open)
                    {
                        Conn.Open();
                    }

                    using (SqlDataReader oReader = objCmd.ExecuteReader())
                    {
                        if (oReader.HasRows)
                        {
                            objList = new DepartmentList();
                            while (oReader.Read())
                            {
                                objList.Add(FillDataRecord(oReader));
                            }
                        }
                        oReader.Close();
                        oReader.Dispose();
                    }
                }
            }
            return objList;
        }

        /// <summary>
        /// This method Saves Record into Database.
        /// </summary>
        /// <param name="objDept">Object containing Data values to be saved.</param>
        /// <returns>Boolean value True if Record is saved successfully
        /// otherwise returns False indicating Record is not saved.</returns>
        public static bool Save(Department objDept)
        {
            int result = 0;
            UserCompany CurrentCompany = new UserCompany();
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                string strSaveQry;
                if (objDept.IsNew)
                {
                    strSaveQry = "INSERT INTO DEPTMAST(DBID, DEPTNAME, DESCRIPTION, ISACTIVE, " +
                        " ST_DATE, MODIFY_DATE, CRBY, MODBY, MACHINENAME) " +
                        "VALUES (@dbId, @DeptName, @Descr, @IsActive, " +
                        " @STDate, @ModifyDate, @CrBy, @ModBy, @MachineName)";
                }
                else
                {
                    strSaveQry = "UPDATE DEPTMAST " +
                        "SET DEPTNAME = @DeptName, DESCRIPTION = @Descr, ISACTIVE = @IsActive, " +
                        "MODIFY_DATE = @ModifyDate, MODBY = @ModBy, MACHINENAME = @MachineName " +
                        "WHERE DBID = @dbId";
                }

                try
                {
                    SqlCommand objCmd = Conn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = strSaveQry;

                    objCmd.Parameters.AddWithValue("@DeptName", objDept.DeptName);
                    objCmd.Parameters.AddWithValue("@Descr", objDept.Description);
                    objCmd.Parameters.AddWithValue("@IsActive", objDept.IsActive);

                    if (objDept.IsNew)
                    {
                        objCmd.Parameters.AddWithValue("@StDate", DateTime.Now);
                        objCmd.Parameters.AddWithValue("@CrBy", CurrentCompany.m_UserName);
                        objDept.DBID = General.GenerateDBID(Conn, "DEPTMAST");
                    }
                    objCmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                    objCmd.Parameters.AddWithValue("@ModBy", CurrentCompany.m_UserName);
                    objCmd.Parameters.AddWithValue("@MachineName", General.GetMachineName());
                    objCmd.Parameters.AddWithValue("@dbID", objDept.DBID);

                    if (Conn.State != ConnectionState.Open)
                    {
                        Conn.Open();
                    }
                    result = objCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return (result > 0);
        }

        /// <summary>
        /// This Method Deletes the record from Database based on ID Specified.
        /// </summary>
        /// <param name="id">Unique ID value for Record.</param>
        /// <returns>Boolean value True if record is Deleted successfully
        /// otherwise returns False indicating record is not Deleted.</returns>
        public static bool Delete(int id)
        {
            int result = 0;

            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                SqlCommand ObjDelCmd = Conn.CreateCommand();
                ObjDelCmd.CommandType = CommandType.Text;
                ObjDelCmd.CommandText = "DELETE FROM DEPTMAST WHERE DBID = @dbID";
                ObjDelCmd.Parameters.AddWithValue("@dbID", id);

                if (Conn.State != ConnectionState.Open)
                {
                    Conn.Open();
                }

                result = ObjDelCmd.ExecuteNonQuery();
                ObjDelCmd.Dispose();
            }
            return (result > 0);
        }

        /// <summary>
        /// This method Checks whether Current Dept already exists in Database or not.
        /// </summary>
        /// <param name="objDept">Object Containing New Data Values.</param>
        /// <returns>Boolean value True if Current Record already exists
        /// otherwise returns False indicating current Record Does not exist.</returns>
        public static bool IsDeptExist(Department objDept)
        {
            bool IsRecordExist = false;
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                try
                {
                    SqlCommand objCmd = Conn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "SELECT DBID FROM DEPTMAST " +
                        " WHERE DEPTNAME = @mDeptName " +
                        " AND DBID <> @dbID ";

                    objCmd.Parameters.AddWithValue("@mDeptName", objDept.DeptName);
                    objCmd.Parameters.AddWithValue("@dbID", objDept.DBID);

                    if (Conn.State != ConnectionState.Open)
                    {
                        Conn.Open();
                    }

                    using (SqlDataReader objReader = objCmd.ExecuteReader())
                    {
                        if (objReader.HasRows)
                        {
                            while (objReader.Read())
                            {
                                IsRecordExist = true;
                            }
                        }
                        else
                        {
                            IsRecordExist = false;
                        }
                    }
                }
                catch (ApplicationException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return IsRecordExist;
        }
    }
}
