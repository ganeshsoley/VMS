using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EntityObject;

namespace DAL
{
    public class VisitorDAL
    {
        #region Private Method(s)
        /// <summary>
        /// Fills values fetched from Database to Object objVisitor.
        /// </summary>
        /// <param name="myDataRec">Record Object containing data values.</param>
        /// <returns>Returns object ObjVisitor containing Data values from Database.</returns>
        private static Visitor FillDataRecord(IDataRecord myDataRec)
        {
            Visitor objVisitor = new Visitor(); 
            
            objVisitor.IsLoading = true;

            objVisitor.DBID = Convert.ToInt32(myDataRec["DBID"]);
            objVisitor.RegNo = Convert.ToInt32(myDataRec["VISITORREGNO"]);
            objVisitor.VName = Convert.ToString(myDataRec["NAME"]);
            objVisitor.Company = Convert.ToString(myDataRec["COMPANY"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("ADDRESS")))
                objVisitor.Address = Convert.ToString(myDataRec["ADDRESS"]);
            objVisitor.MobileNo = Convert.ToString(myDataRec["CONTACTNO"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("IDPROOFTYPE")))
                objVisitor.IDProofType = Convert.ToString(myDataRec["IDPROOFTYPE"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("IDPROOFNO")))
                objVisitor.IDProofNo = Convert.ToString(myDataRec["IDPROOFNO"]);

            objVisitor.IsNew = false;
            objVisitor.IsEdited = false;
            objVisitor.IsDeleted = false;
            objVisitor.IsLoading = false;

            return objVisitor;
        }
        #endregion

        #region Public Method(s)
        /// <summary>
        /// This method retrieves "Visitor" Record, which is retrieved from Database.
        /// </summary>
        /// <param name="dbid">Unique ID value based on which Record will be fetched from Database.</param>
        /// <returns>Object "Visitor" containing Data Values.</returns>
        public static Visitor GetItem(int dbid)
        {
            Visitor objVisitor = null; 
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    try
                    {
                        objCmd.Connection = Conn;
                        objCmd.CommandType = CommandType.Text;
                        objCmd.CommandText = "SELECT * FROM VISITORMASTER " +
                            " WHERE DBID = @mDBID";
                        objCmd.Parameters.AddWithValue("@mDBID", dbid);

                        if (Conn.State != ConnectionState.Open)
                        {
                            Conn.Open();
                        }

                        SqlDataReader oReader = objCmd.ExecuteReader();
                        if (oReader.Read())
                        {
                            objVisitor = FillDataRecord(oReader);
                            objVisitor.IsNew = false;
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
            return objVisitor;
        }

        /// <summary>
        /// This method provides List of Visitors available in Database.
        /// </summary>
        /// <param name="strWhere">Specifies condition for retrieving records.</param>
        /// <returns>Collection of Visitor Objects.</returns>
        public static VisitorList GetList(string strCompany, string strContactNo)
        {

            VisitorList objList = null; 
            string strSql = "Select * from VISITORMASTER ";

            if (strCompany != string.Empty & strContactNo == string.Empty)
            {
                strSql += " WHERE COMPANY = '" + strCompany + "'";
            }
            else if (strCompany == string.Empty & strContactNo != string.Empty)
            {
                strSql += " WHERE CONTACTNO = '" + strContactNo + "'";
            }
            else if ((strCompany != string.Empty) & (strContactNo != string.Empty))
            {
                strSql += " WHERE COMPANY = '" + strCompany +
                       "' AND CONTACTNO = '" + strContactNo + "'";
            }

            strSql += " ORDER BY VISITORREGNO";

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
                            objList = new VisitorList();
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
        /// <param name="objVisitor">Object containing Data values to be saved.</param>
        /// <returns>Boolean value True if Record is saved successfully
        /// otherwise returns False indicating Record is not saved.</returns>
        public static bool Save(Visitor objVisitor)
        {
            int result = 0;
            UserCompany CurrentCompany = new UserCompany();
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                string strSaveQry;
                if (objVisitor.IsNew)
                {
                    strSaveQry = "INSERT INTO VISITORMASTER(DBID, VISITORREGNO, NAME, COMPANY, " +
                        " ADDRESS, CONTACTNO, IDPROOFTYPE, IDPROOFNO, " +
                        " ST_DATE, MODIFY_DATE, CRBY, MODBY, MACHINENAME) " +
                        " VALUES (@dbId, @VisitorRegNo, @Name, @Company, " +
                        " @Address, @ContactNo, @IdProofType, @IdProofNo, " +
                        " @STDate, @ModifyDate, @CrBy, @ModBy, @MachineName)";
                }
                else
                {
                    strSaveQry = "UPDATE VISITORMASTER " +
                        " SET VISITORREGNO = @VisitorRegNo, NAME = @Name, COMPANY = @Company, ADDRESS = @Address, " +
                        " CONTACTNO = @ContactNo, IDPROOFTYPE = @IdProofType, IDPROOFNO = @IdProofNo, " +
                        " MODIFY_DATE = @ModifyDate, MODBY = @ModBy, MACHINENAME = @MachineName " +
                        " WHERE DBID = @dbId";
                }

                try
                {
                    SqlCommand objCmd = Conn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = strSaveQry;

                    objCmd.Parameters.AddWithValue("@Name", objVisitor.VName);
                    objCmd.Parameters.AddWithValue("@Company", objVisitor.Company);
                    objCmd.Parameters.AddWithValue("@Address", objVisitor.Address);
                    objCmd.Parameters.AddWithValue("@ContactNo", objVisitor.MobileNo);
                    objCmd.Parameters.AddWithValue("@IDProofType", objVisitor.IDProofType);
                    objCmd.Parameters.AddWithValue("@IDProofNo", objVisitor.IDProofNo);

                    if (objVisitor.IsNew)
                    {
                        objCmd.Parameters.AddWithValue("@StDate", DateTime.Now);
                        objCmd.Parameters.AddWithValue("@CrBy", CurrentCompany.m_UserName);
                        //objVisitor.DBID = General.GenerateDBID(Conn, "VISITORMASTER");
                        objVisitor.DBID = General.GenerateDBID("SEQVISITORID", Conn);
                        objVisitor.RegNo = objVisitor.DBID;
                    }
                    objCmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                    objCmd.Parameters.AddWithValue("@ModBy", CurrentCompany.m_UserName);
                    objCmd.Parameters.AddWithValue("@MachineName", General.GetMachineName());
                    objCmd.Parameters.AddWithValue("@dbID", objVisitor.DBID);
                    objCmd.Parameters.AddWithValue("@VisitorRegNo", objVisitor.RegNo);

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
                ObjDelCmd.CommandText = "DELETE FROM VISITORMASTER WHERE DBID = @dbID";
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
        /// Returns Companys list from Visitor Table.
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCompanys()
        {
            string strQry;
            DataTable dTable = null;

            strQry = "SELECT DISTINCT COMPANY " +
                " FROM VISITORMASTER " +
                " Order by COMPANY ";

            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                using (SqlCommand cmd = Conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strQry;

                    try
                    {
                        if (Conn.State != ConnectionState.Open)
                        {
                            Conn.Open();
                        }
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dTable = new DataTable();
                            da.Fill(dTable);
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            return dTable;

        }

        /// <summary>
        /// Returns Contacts List from Visitor Table.
        /// </summary>
        /// <returns></returns>
        public static DataTable GetContacts()
        {
            string strQry;
            DataTable dTable = null;

            strQry = "SELECT DISTINCT CONTACTNO " +
                " FROM VISITORMASTER " +
                " Order by CONTACTNO ";

            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                using (SqlCommand cmd = Conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strQry;

                    try
                    {
                        if (Conn.State != ConnectionState.Open)
                        {
                            Conn.Open();
                        }
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dTable = new DataTable();
                            da.Fill(dTable);
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            return dTable;
        }

        /// <summary>
        /// Returns Registration No. For New Visitor
        /// </summary>
        /// <returns></returns>
        public static int GetVRegNo()
        {
            int result = 0;

            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                SqlCommand ObjCmd = Conn.CreateCommand();
                ObjCmd.CommandType = CommandType.Text;
                ObjCmd.CommandText = "select Max(VISITORREGNO) VREGNO " +
                    " from visitormaster ";

                if (Conn.State != ConnectionState.Open)
                {
                    Conn.Open();
                }

                object objTemp = ObjCmd.ExecuteScalar();
                if (objTemp == null || objTemp == DBNull.Value)
                {
                    result = 1;
                }
                else
                {
                    result = Convert.ToInt32(objTemp);
                    result += 1;
                }

                ObjCmd.Dispose();
            }
            return result;
        }
        #endregion

    }
}
