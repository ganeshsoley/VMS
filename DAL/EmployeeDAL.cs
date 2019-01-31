using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EntityObject;

namespace DAL
{
    public class EmployeeDAL
    {
        #region Private Method(s)
        /// <summary>
        /// Fills values fetched from Database to Object objDept.
        /// </summary>
        /// <param name="myDataRec">Record Object containing data values.</param>
        /// <returns>Returns object ObjEmployee containing Data values from Database.</returns>
        private static Employee FillDataRecord(IDataRecord myDataRec)
        {
            Employee objEmp = new Employee();
            objEmp.IsLoading = true;
            objEmp.DBID = Convert.ToInt32(myDataRec["DBID"]);
            objEmp.EmpCode = Convert.ToString(myDataRec["EMPCODE"]);
            objEmp.FirstName = Convert.ToString(myDataRec["FIRSTNAME"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("MIDDLENAME")))
                objEmp.MiddleName = Convert.ToString(myDataRec["MIDDLENAME"]);
            objEmp.LastName = Convert.ToString(myDataRec["LASTNAME"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("INITIALS")))
                objEmp.Initials = Convert.ToString(myDataRec["INITIALS"]);
            objEmp.DeptID = Convert.ToInt32(myDataRec["DEPT"]);
            objEmp.DeptName = Convert.ToString(myDataRec["DEPTNAME"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("JOINDATE")))
                objEmp.JoinDate = Convert.ToDateTime(myDataRec["JOINDATE"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("BIRTHDATE")))
                objEmp.BirthDate = Convert.ToDateTime(myDataRec["BIRTHDATE"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("LEFTDATE")))
                objEmp.LeftDate = Convert.ToDateTime(myDataRec["LEFTDATE"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("MOBILENO")))
                objEmp.MobileNo = Convert.ToString(myDataRec["MOBILENO"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("EMAILID")))
                objEmp.EMailID = Convert.ToString(myDataRec["EMAILID"]);
            objEmp.InActive = Convert.ToInt32(myDataRec["INACTIVE"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("EMPPLANT")))
                objEmp.EmpPlant = Convert.ToString(myDataRec["EMPPLANT"]);

            objEmp.IsNew = false;
            objEmp.IsEdited = false;
            objEmp.IsDeleted = false;
            objEmp.IsLoading = false;

            return objEmp;
        }
        #endregion

        /// <summary>
        /// This method retrieves "Employee" Record, from Database.
        /// </summary>
        /// <param name="dbid">Unique ID value based on which Record will be fetched.</param>
        /// <returns>Object "Employee" containing Data Values.</returns>
        public static Employee GetItem(long dbid)
        {
            Employee objEmp = null;
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    try
                    {
                        objCmd.Connection = Conn;
                        objCmd.CommandType = CommandType.Text;
                        objCmd.CommandText = "SELECT a.*, b.DeptName " +
                            " FROM EMPMast a, DEPTMAST b" +
                            " WHERE a.dept = b.dbid " +
                            " and a.DBID = @mDBID";
                        objCmd.Parameters.AddWithValue("@mDBID", dbid);

                        if (Conn.State != ConnectionState.Open)
                        {
                            Conn.Open();
                        }

                        SqlDataReader oReader = objCmd.ExecuteReader();
                        if (oReader.Read())
                        {
                            objEmp = FillDataRecord(oReader);
                            objEmp.IsNew = false;
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
            return objEmp;
        }

        /// <summary>
        /// This method provides List of Employees available in Database.
        /// </summary>
        /// <param name="strWhere">Specifies condition for retrieving records.</param>
        /// <returns>Collection of Employee Objects.</returns>
        public static EmployeeList GetList(string strWhere)
        {
            EmployeeList objList = null;
            string strSql = "SELECT A.*, B.DEPTNAME " +
                " FROM EmpMast A, DEPTMAST B " +
                " WHERE A.DEPT = B.DBID ";//(+)

            if (strWhere != string.Empty)
                strSql = strSql + " AND " + strWhere;
            strSql += " ORDER BY DEPTNAME, INITIALS";

            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                using (SqlCommand objCmd = new SqlCommand())
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
                            objList = new EmployeeList();
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
        /// <param name="objEmp">Object containing Data values to be saved.</param>
        /// <returns>Boolean value True if Record is saved successfully
        /// otherwise returns False indicating Record is not saved.</returns>
        public static bool Save(Employee objEmp, User objUser)
        {
            int result = 0;
            UserCompany CurrentCompany = new UserCompany();
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                string strSaveQry;
                if (objEmp.IsNew)
                {
                    strSaveQry = "INSERT INTO EMPMAST(DBID, EMPCODE, FIRSTNAME, MIDDLENAME, LASTNAME, " +
                        " INITIALS, DEPT, MOBILENO, EMAILID, INACTIVE, " +
                        " EMPPLANT, ST_DATE, MODIFY_DATE, CRBY, MODBY, MACHINENAME) " +
                        " VALUES (@dbId, @EMPCODE, @FIRSTNAME, @MIDDLENAME, @LASTNAME,  " +
                        " @INITIALS, @DEPT, @MOBILENO, @EMAILID, @INACTIVE, " +
                        " @EMPPLANT, @STDate, @ModifyDate, @CrBy, @ModBy, @MachineName)";
                }
                else
                {
                    strSaveQry = "UPDATE EMPMAST " +
                        " SET EMPCODE = @EMPCODE, FIRSTNAME = @FIRSTNAME, MIDDLENAME = @MIDDLENAME, " +
                        " LASTNAME = @LASTNAME, INITIALS = @INITIALS, DEPT = @DEPT, " +
                        " MOBILENO = @MOBILENO, EMAILID = @EMAILID, INACTIVE = @INACTIVE, " +
                        " EMPPLANT = @EMPPLANT, " +
                        " MODIFY_DATE = @ModifyDate, MODBY = @ModBy, MACHINENAME = @MachineName " +
                        " WHERE DBID = @dbId";
                }

                try
                {
                    SqlCommand objCmd = Conn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = strSaveQry;

                    objCmd.Parameters.AddWithValue("@EMPCODE", objEmp.EmpCode);
                    objCmd.Parameters.AddWithValue("@FIRSTNAME", objEmp.FirstName);
                    objCmd.Parameters.AddWithValue("@MIDDLENAME", objEmp.MiddleName);
                    objCmd.Parameters.AddWithValue("@LASTNAME", objEmp.LastName);
                    objCmd.Parameters.AddWithValue("@INITIALS", objEmp.Initials);
                    objCmd.Parameters.AddWithValue("@DEPT", objEmp.DeptID);
                    //objCmd.Parameters.AddWithValue("@JOINDATE", objEmp.JoinDate);
                    //objCmd.Parameters.AddWithValue("@BIRTHDATE", objEmp.BirthDate);
                    //objCmd.Parameters.AddWithValue("@LEFTDATE", objEmp.LeftDate);
                    objCmd.Parameters.AddWithValue("@MOBILENO", objEmp.MobileNo);
                    objCmd.Parameters.AddWithValue("@EMAILID", objEmp.EMailID);
                    objCmd.Parameters.AddWithValue("@INACTIVE", objEmp.InActive);
                    objCmd.Parameters.AddWithValue("@EMPPLANT", objEmp.EmpPlant);

                    if (objEmp.IsNew)
                    {
                        objCmd.Parameters.AddWithValue("@StDate", DateTime.Now);
                        objCmd.Parameters.AddWithValue("@CrBy", objUser.LoginName);
                        //objEmp.DBID = General.GenerateDBID(Conn, "EMPMAST");
                        objEmp.DBID = General.GenerateDBID("SEQEMPID", Conn);
                    }
                    objCmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                    objCmd.Parameters.AddWithValue("@ModBy", objUser.LoginName);
                    objCmd.Parameters.AddWithValue("@MachineName", General.GetMachineName());
                    objCmd.Parameters.AddWithValue("@dbID", objEmp.DBID);

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
        public static bool Delete(long id)
        {
            int result = 0;

            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                SqlCommand ObjDelCmd = Conn.CreateCommand();
                ObjDelCmd.CommandType = CommandType.Text;
                ObjDelCmd.CommandText = "DELETE FROM EMPMAST WHERE DBID = @dbID";
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
        /// This method Checks whether Current Employee already exists in Database or not.
        /// </summary>
        /// <param name="objEmp">Object Containing New Data Values.</param>
        /// <returns>Boolean value True if Current Record already exists
        /// otherwise returns False indicating current Record Does not exist.</returns>
        public static bool IsEmployeeExist(Employee objEmp)
        {
            bool IsRecordExist = false;
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                try
                {
                    SqlCommand objCmd = Conn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "SELECT DBID FROM EMPMAST " +
                        " WHERE FIRSTNAME = @mFName " +
                        " AND MIDDLENAME = @mMidName " +
                        " AND LASTNAME = @mLName " +
                        " AND DBID <> @dbID ";

                    objCmd.Parameters.AddWithValue("@mFName", objEmp.FirstName);
                    objCmd.Parameters.AddWithValue("@mMidName", objEmp.MiddleName);
                    objCmd.Parameters.AddWithValue("@mLName", objEmp.LastName);
                    objCmd.Parameters.AddWithValue("@dbID", objEmp.DBID);

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
