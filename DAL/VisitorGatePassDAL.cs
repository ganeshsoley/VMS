using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityObject;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class VisitorGatePassDAL
    {
        #region Private Method(s)
        /// <summary>
        /// Fills values fetched from Database to Object objVisitorGP
        /// </summary>
        /// <param name="myDataRec">Record Object containing data values.</param>
        /// <returns>Returns object ObjVisitorGP containing Data values from Database.</returns>
        private static VisitorGatePass FillDataRecord(IDataRecord myDataRec)
        {
            VisitorGatePass objVisitorGP = new VisitorGatePass();
            objVisitorGP.IsLoading = true;
            objVisitorGP.DBID = Convert.ToInt32(myDataRec["DBID"]);
            objVisitorGP.GatePassNo = Convert.ToString(myDataRec["GATEPASSNO"]);
            objVisitorGP.GateDate = Convert.ToDateTime(myDataRec["GATEDATE"]);
            objVisitorGP.VisitorID = Convert.ToInt32(myDataRec["VISITORID"]);
            objVisitorGP.VisitorName = Convert.ToString(myDataRec["NAME"]);
            objVisitorGP.Address = Convert.ToString(myDataRec["ADDRESS"]);
            objVisitorGP.ContactNo = Convert.ToString(myDataRec["CONTACTNO"]);

            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("VEHICLENO")))
                objVisitorGP.VehicleNo = Convert.ToString(myDataRec["VEHICLENO"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("DEPOSITEDMATERIAL")))
                objVisitorGP.DepositMaterial = Convert.ToString(myDataRec["DEPOSITEDMATERIAL"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("CARRYMATERIAL")))
                objVisitorGP.CarryMaterial = Convert.ToString(myDataRec["CARRYMATERIAL"]);
            
            objVisitorGP.NoOfPersons = Convert.ToInt32(myDataRec["NOOFPERSONS"]);
            objVisitorGP.CompanyName = Convert.ToString(myDataRec["COMPANY"]);
            objVisitorGP.EmployeeID = Convert.ToInt32(myDataRec["EMPID"]);
            objVisitorGP.ToMeet = Convert.ToString(myDataRec["TOMEET"]);
            objVisitorGP.Purpose = Convert.ToString(myDataRec["PURPOSE"]);
            objVisitorGP.TimeIn = Convert.ToDateTime(myDataRec["TIMEIN"]);

            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("TIMEOUT")))
                objVisitorGP.TimeOut = Convert.ToDateTime(myDataRec["TIMEOUT"]);
            //objVisitorGP.ImgFileName = Convert.ToString(myDataRec["IMGFILENAME"]);
            
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("APPOINTMENTNO")))
                objVisitorGP.AppointmentNo = Convert.ToString(myDataRec["APPOINTMENTNO"]);
            objVisitorGP.EmpMobile = Convert.ToString(myDataRec["EMPMOBILE"]);
            
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("EXPERSONS")))
                objVisitorGP.ExtraPersons = Convert.ToInt32(myDataRec["EXPERSONS"]);

            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("VIMAGE")))
                objVisitorGP.VisitorImage = (byte[])myDataRec["VIMAGE"];

            objVisitorGP.IsNew = false;
            objVisitorGP.IsEdited = false;
            objVisitorGP.IsDeleted = false;
            objVisitorGP.IsLoading = false;

            return objVisitorGP;
        }
        #endregion

        /// <summary>
        /// This method retrieves "VisitorGatePass" Record from Database.
        /// </summary>
        /// <param name="dbid">Unique ID value based on which Record will be fetched.</param>
        /// <returns>Object "VisitorGatePass" containing Data Values.</returns>
        public static VisitorGatePass GetItem(int dbid)
        {
            VisitorGatePass objVisitorGP = null;
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    try
                    {
                        objCmd.Connection = Conn;
                        objCmd.CommandType = CommandType.Text;
                        objCmd.CommandText = "SELECT * FROM VISITORGATEPASS " +
                            " WHERE DBID = @DBID";
                        objCmd.Parameters.AddWithValue("@DBID", dbid);

                        if (Conn.State != ConnectionState.Open)
                        {
                            Conn.Open();
                        }

                        SqlDataReader oReader = objCmd.ExecuteReader();
                        if (oReader.Read())
                        {
                            objVisitorGP = FillDataRecord(oReader);
                            objVisitorGP.IsNew = false;
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
            return objVisitorGP;
        }

        /// <summary>
        /// This method provides List of Visitor-GatePass available in Database.
        /// </summary>
        /// <param name="strWhere">Specifies condition for retrieving records.</param>
        /// <returns>Collection of Visitor GatePass Objects.</returns>
        public static VisitorGatePassList GetList(string strWhere, DateTime vwDate, bool flgShowAll)
        {
            VisitorGatePassList objList = null;
            string strSql = "Select * from VISITORGATEPASS ";

            if (!flgShowAll)
            {
                strSql += " WHERE GATEDATE = @gateDate ";

                if (strWhere != string.Empty)
                    strSql = strSql + " AND " + strWhere;
            }
            else
            {
                if (strWhere != string.Empty)
                    strSql = strSql + " WHERE " + strWhere;
            }
            strSql += " ORDER BY GATEDATE DESC, GATEPASSNO DESC";

            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    objCmd.Connection = Conn;
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = strSql;
                    if (!flgShowAll)
                    {
                        objCmd.Parameters.AddWithValue("@gateDate", vwDate.ToString("dd-MMM-yy"));
                    }

                    if (Conn.State != ConnectionState.Open)
                    {
                        Conn.Open();
                    }

                    using (SqlDataReader oReader = objCmd.ExecuteReader())
                    {
                        if (oReader.HasRows)
                        {
                            objList = new VisitorGatePassList();
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
        public static bool Save(VisitorGatePass objVisitorGP)
        {
            int result = 0;
            //string tmpStr = "";
            DateTime dt = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            UserCompany CurrentCompany = new UserCompany();
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                string strSaveQry;
                if (objVisitorGP.IsNew)
                {
                    strSaveQry = "INSERT INTO VISITORGATEPASS(dbid, gatePassNo, gateDate, " +
                        " visitorID, Name, address, contactNo, vehicleNo, depositedMaterial, " +
                        " carryMaterial, NoOfPersons, company, empID, toMeet, purpose, " +
                        " timeIn, timeOut, appointmentNo, empMobile, exPersons, " +
                        " ST_DATE, MODIFY_DATE, CRBY, MODBY, MACHINENAME, VIMAGE) " +
                        " VALUES (@dbid, @gatePassNo, @gateDate, @visitorID, @visitorName, " +
                        " @address, @contactNo, @vehicleNo, @depositMaterial, " +
                        " @carryMaterial, @NoOfPerson, @companyName, @empID, @toMeet, @purpose, " +
                        " @timeIn, @timeOut, @appointmentNo, @empMobile, @exPersons, " +
                        " @STDate, @ModifyDate, @CrBy, @ModBy, @MachineName, @VImage)";
                }
                else
                {
                    strSaveQry = "UPDATE VISITORGATEPASS " +
                        " SET gatePassNo = @GatePassNo, gateDate = @GateDate, visitorID = @visitorID, " +
                        " Name = @visitorName, address = @address, contactNo = @contactNo, " +
                        " vehicleNo = @vehicleNo, depositedMaterial = @depositMaterial, " +
                        " carryMaterial = @carryMaterial, NoOfPersons = @NoOfPerson, company = @companyName, " +
                        " empID = @empID, toMeet = @toMeet, purpose = @purpose, timeIn = @timeIn, " +
                        " timeOut = @timeOut, appointmentNo = @appointmentNo, " +
                        " empMobile = @empMobile, exPersons = @exPersons " +
                        " WHERE DBID = @dbId";
                }

                try
                {
                    SqlCommand objCmd = Conn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = strSaveQry;

                    objCmd.Parameters.AddWithValue("@gateDate", objVisitorGP.GateDate);
                    objCmd.Parameters.AddWithValue("@visitorID", objVisitorGP.VisitorID);
                    objCmd.Parameters.AddWithValue("@visitorName", objVisitorGP.VisitorName);
                    objCmd.Parameters.AddWithValue("@address", objVisitorGP.Address);
                    objCmd.Parameters.AddWithValue("@contactNo", objVisitorGP.ContactNo);
                    objCmd.Parameters.AddWithValue("@vehicleNo", objVisitorGP.VehicleNo);
                    objCmd.Parameters.AddWithValue("@depositMaterial", objVisitorGP.DepositMaterial);
                    objCmd.Parameters.AddWithValue("@carryMaterial", objVisitorGP.CarryMaterial);
                    objCmd.Parameters.AddWithValue("@NoOfPerson", objVisitorGP.NoOfPersons);
                    objCmd.Parameters.AddWithValue("@companyName", objVisitorGP.CompanyName);
                    objCmd.Parameters.AddWithValue("@empID", objVisitorGP.EmployeeID);
                    objCmd.Parameters.AddWithValue("@toMeet", objVisitorGP.ToMeet);
                    objCmd.Parameters.AddWithValue("@purpose", objVisitorGP.Purpose);
                    objCmd.Parameters.AddWithValue("@timeIn", objVisitorGP.TimeIn);

                    if (objVisitorGP.TimeOut == DateTime.MinValue)
                        objCmd.Parameters.AddWithValue("@timeOut", DBNull.Value);
                    else
                        objCmd.Parameters.AddWithValue("@timeOut", objVisitorGP.TimeOut);

                    //objCmd.Parameters.AddWithValue("@imgFileName", objVisitorGP.ImgFileName);
                    objCmd.Parameters.AddWithValue("@appointmentNo", objVisitorGP.AppointmentNo);
                    objCmd.Parameters.AddWithValue("@empMobile", objVisitorGP.EmpMobile);
                    objCmd.Parameters.AddWithValue("@exPersons", objVisitorGP.ExtraPersons);

                    if (objVisitorGP.IsNew)
                    {
                        objCmd.Parameters.AddWithValue("@StDate", DateTime.Now);
                        objCmd.Parameters.AddWithValue("@CrBy", CurrentCompany.m_UserName);
                        objCmd.Parameters.AddWithValue("@VImage", objVisitorGP.VisitorImage);

                        objVisitorGP.DBID = General.GenerateDBID(Conn, "VISITORGATEPASS");
                        objVisitorGP.GatePassNo = string.Format("{0:yyMMdd}", dt) + Convert.ToString(objVisitorGP.DBID).PadLeft(4, Convert.ToChar(48));
                    }
                    objCmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                    objCmd.Parameters.AddWithValue("@ModBy", CurrentCompany.m_UserName);
                    objCmd.Parameters.AddWithValue("@MachineName", General.GetMachineName());

                    objCmd.Parameters.AddWithValue("@gatePassNo", objVisitorGP.GatePassNo);
                    objCmd.Parameters.AddWithValue("@dbID", objVisitorGP.DBID);

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
                ObjDelCmd.CommandText = "DELETE FROM VISITORGATEPASS WHERE DBID = @dbID";
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
        /// <param name="objVisitorGP">Object Containing New Data Values.</param>
        /// <returns>Boolean value True if Current Record already exists
        /// otherwise returns False indicating current Record Does not exist.</returns>
        public static bool IsGatePassExist(VisitorGatePass objVisitorGP)
        {
            bool IsRecExist = false;
            //using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            //{
            //    try
            //    {
            //        SqlCommand objCmd = Conn.CreateCommand();
            //        objCmd.CommandType = CommandType.Text;
            //        objCmd.CommandText = "SELECT DBID FROM VISITORGATEPASS " +
            //            " WHERE DEPTNAME = @mDeptName " +
            //            " AND DBID <> @dbID ";

            //        objCmd.Parameters.AddWithValue("@mDeptName", objVisitorGatePass.DeptName);
            //        objCmd.Parameters.AddWithValue("@dbID", objVisitorGatePass.DBID);

            //        if (Conn.State != ConnectionState.Open)
            //        {
            //            Conn.Open();
            //        }

            //        using (SqlDataReader objReader = objCmd.ExecuteReader())
            //        {
            //            if (objReader.HasRows)
            //            {
            //                while (objReader.Read())
            //                {
            //                    IsRecExist = true;
            //                }
            //            }
            //            else
            //            {
            //                IsRecExist = false;
            //            }
            //        }
            //    }
            //    catch (ApplicationException ex)
            //    {
            //        throw new Exception(ex.Message);
            //    }
            //}
            return IsRecExist;
        }

        /// <summary>
        /// Gives next GatePass EntryNo.
        /// </summary>
        /// <param name="Conn">Connection Object</param>
        /// <param name="TabName">Table Name for which DBID is to be generated.</param>
        /// <returns>Integer Unique Value for Table.</returns>
        public static int GetNextGPNo(SqlConnection Conn)
        {
            int id;
            using (SqlCommand objCmd = new SqlCommand())
            {
                try
                {
                    objCmd.Connection = Conn;
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "SELECT max(GATEPASSNO) NO FROM VISITORGATEPASS ";
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

        public static DataTable GetVisitorImage(long vDBID)
        {
            DataTable dTable = null;
            //VisitorGatePass objVisitorGP = null;
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    try
                    {
                        objCmd.Connection = Conn;
                        objCmd.CommandType = CommandType.Text;
                        objCmd.CommandText = "SELECT DBID, VISITORID, VIMAGE " +
                            " FROM VISITORGATEPASS " +
                            " WHERE VISITORID = @VisitorID " +
                            " AND DBID = (SELECT MAX(DBID) FROM VISITORGATEPASS " +
                            " WHERE VISITORID = @VisitorID)";
                        objCmd.Parameters.AddWithValue("@VisitorID", vDBID);

                        if (Conn.State != ConnectionState.Open)
                        {
                            Conn.Open();
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(objCmd))
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
        /// Close Appointment Used for Visitor GatePass
        /// </summary>
        /// <param name="objVisitorGP"></param>
        public static void CloseAppointment(VisitorGatePass objVisitorGP)
        {
            int result = 0;
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    try
                    {
                        objCmd.Connection = Conn;
                        objCmd.CommandType = CommandType.Text;
                        objCmd.CommandText = "UPDATE APPOINTMENTMASTER " +
                            " SET APMTCLOSE = @value " +
                            " WHERE APPOINTMENTNO = @AppointmentNo ";
                        objCmd.Parameters.AddWithValue("@value", true);
                        objCmd.Parameters.AddWithValue("@AppointmentNo", objVisitorGP.AppointmentNo);

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
            }
        }

        /// <summary>
        /// Open an Appointment when a VisitorGP is Deleted.
        /// </summary>
        /// <param name="objVisitorGP"></param>
        public static void ReOpenAppointment(string strApmtNo)
        {
            int result = 0;
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    try
                    {
                        objCmd.Connection = Conn;
                        objCmd.CommandType = CommandType.Text;
                        objCmd.CommandText = "UPDATE APPOINTMENTMASTER " +
                            " SET APMTCLOSE = @value " +
                            " WHERE APPOINTMENTNO = @AppointmentNo ";
                        objCmd.Parameters.AddWithValue("@value", false);
                        objCmd.Parameters.AddWithValue("@AppointmentNo", strApmtNo);

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
            }
        }

        public static DataTable GetEmployee()
        {
            string strQry;
            DataTable dTable = null;

            strQry = "select distinct tomeet from VISITORGATEPASS " +
                " where TOMEET is not null " +
                " Order by tomeet ";

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

    }
}
