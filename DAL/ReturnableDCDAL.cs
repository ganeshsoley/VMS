using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EntityObject;
using System.Linq;
using System.Text;

namespace DAL
{
    public class ReturnableDCDAL
    {
        #region Private Method(s)
        /// <summary>
        /// Fills values fetched from Database to Object objDept.
        /// </summary>
        /// <param name="myDataRec">Record Object containing data values.</param>
        /// <returns>Returns object ObjDept containing Data values from Database.</returns>
        private static ReturnableDC FillDataRecord(IDataRecord myDataRec)
        {
            ReturnableDC objRetDC = new ReturnableDC();
            objRetDC.IsLoading = true;
            objRetDC.DBID = Convert.ToInt32(myDataRec["DBID"]);
            objRetDC.EntryNo = Convert.ToInt32(myDataRec["ENTRYNO"]);
            objRetDC.EntryDate = Convert.ToDateTime(myDataRec["ENTRYDATE"]);
            objRetDC.EntryType = Convert.ToString(myDataRec["ENTRYTYPE"]);
            objRetDC.PartyName = Convert.ToString(myDataRec["PARTYNAME"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("vehicleno")))
                objRetDC.VehicleNo = Convert.ToString(myDataRec["VEHICLENO"]);
            objRetDC.DCNo = Convert.ToString(myDataRec["DCNO"]);
            objRetDC.DCDate = Convert.ToDateTime(myDataRec["DCDATE"]);
            objRetDC.PlantName = Convert.ToString(myDataRec["PLANT"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("vindate")))
                objRetDC.VInDate = Convert.ToDateTime(myDataRec["VINDATE"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("vintime")))
                objRetDC.VInTime = Convert.ToDateTime(myDataRec["VINTIME"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("voutdate")))
                objRetDC.VOutDate = Convert.ToDateTime(myDataRec["VOUTDATE"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("vouttime")))
                objRetDC.VOutTime = Convert.ToDateTime(myDataRec["VOUTTIME"]);

            objRetDC.IsNew = false;
            objRetDC.IsEdited = false;
            objRetDC.IsDeleted = false;
            objRetDC.IsLoading = false;

            return objRetDC;
        }
        #endregion

        /// <summary>
        /// This method retrieves "ReturnableDC" Record from Database.
        /// </summary>
        /// <param name="dbid">Unique ID value based on which Record will be fetched from Database.</param>
        /// <returns>Object "ReturnableDC" containing Data Values.</returns>
        public static ReturnableDC GetItem(int dbid)
        {
            ReturnableDC objRetDC = null;
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    try
                    {
                        objCmd.Connection = Conn;
                        objCmd.CommandType = CommandType.Text;
                        objCmd.CommandText = "SELECT * FROM RETURNABLEDC " +
                            " WHERE DBID = @DBID";
                        objCmd.Parameters.AddWithValue("@DBID", dbid);

                        if (Conn.State != ConnectionState.Open)
                        {
                            Conn.Open();
                        }

                        SqlDataReader oReader = objCmd.ExecuteReader();
                        if (oReader.Read())
                        {
                            objRetDC = FillDataRecord(oReader);
                            objRetDC.IsNew = false;
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
            return objRetDC;
        }

        /// <summary>
        /// This method provides List of Returnable DCs available in Database.
        /// </summary>
        /// <param name="strWhere">Specifies condition for retrieving records.</param>
        /// <returns>Collection of Returnable DC Objects.</returns>
        public static ReturnableDCList GetList(string entryType, DateTime lvDate, bool flgShowAll)
        {
            ReturnableDCList objList = null;
            string strSql = "SELECT * " +
                " FROM ReturnableDC " +
                " WHERE ENTRYTYPE = @entryType ";
            if (!flgShowAll)
            {
                strSql += " AND ENTRYDATE = @entryDate ";
            }
            
            strSql += " ORDER BY ENTRYDATE DESC, ENTRYNO DESC";

            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    objCmd.Connection = Conn;
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = strSql;
                    objCmd.Parameters.AddWithValue("@entryType", entryType);
                    
                    if (!flgShowAll)
                    {
                        objCmd.Parameters.AddWithValue("@entryDate", lvDate.ToString("dd-MMM-yy"));
                    }

                    if (Conn.State != ConnectionState.Open)
                    {
                        Conn.Open();
                    }

                    using (SqlDataReader oReader = objCmd.ExecuteReader())
                    {
                        if (oReader.HasRows)
                        {
                            objList = new ReturnableDCList();
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
        /// <param name="objRetDC">Object containing Data values to be saved.</param>
        /// <returns>Boolean value True if Record is saved successfully
        /// otherwise returns False indicating Record is not saved.</returns>
        public static bool Save(ReturnableDC objRetDC, User currentUser)
        {
            int result = 0;
            UserCompany CurrentCompany = new UserCompany();
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                string strSaveQry;
                if (objRetDC.IsNew)
                {
                    strSaveQry = "INSERT INTO RETURNABLEDC(DBID, ENTRYNO, ENTRYDATE, ENTRYTYPE, " +
                        " PARTYNAME, VEHICLENO, DCNO, DCDATE, PLANT, VINDATE, VINTIME, VOUTDATE, " +
                        " VOUTTIME, ST_DATE, MODIFY_DATE, CRBY, MODBY, MACHINENAME) " +
                        " VALUES (@dbId, @EntryNo, @EntryDate, @EntryType, @PartyName, @VehicleNo, " +
                        " @DCNo, @DCDate, @Plant, @VinDate, @VinTime, @VOutDate, @VoutTime, " +
                        " @STDate, @ModifyDate, @CrBy, @ModBy, @MachineName)";
                }
                else
                {
                    strSaveQry = "UPDATE RETURNABLEDC " +
                        " SET ENTRYNO = @EntryNo, ENTRYDATE = @EntryDate, ENTRYTYPE = @EntryType, " +
                        " PARTYNAME = @PartyName, VEHICLENO = @VehicleNo, DCNO = @DCNo, " +
                        " DCDATE = @DCDate, PLANT = @Plant, VINDATE = @VInDate, VINTIME = @VInTime, " +
                        " VOUTDATE = @VOutDate, VOUTTIME = @VOutTime, MODIFY_DATE = @ModifyDate, " +
                        " MODBY = @ModBy, MACHINENAME = @MachineName " +
                        " WHERE DBID = @dbId";
                }

                try
                {
                    SqlCommand objCmd = Conn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = strSaveQry;

                    objCmd.Parameters.AddWithValue("@EntryDate", objRetDC.EntryDate);
                    objCmd.Parameters.AddWithValue("@EntryType", objRetDC.EntryType);
                    objCmd.Parameters.AddWithValue("@PartyName", objRetDC.PartyName);
                    if (objRetDC.VehicleNo == string.Empty)
                        objCmd.Parameters.AddWithValue("@VehicleNo", DBNull.Value);
                    else
                        objCmd.Parameters.AddWithValue("@VehicleNo", objRetDC.VehicleNo);
                    objCmd.Parameters.AddWithValue("@DCNo", objRetDC.DCNo);
                    objCmd.Parameters.AddWithValue("@DCDate", objRetDC.DCDate);
                    objCmd.Parameters.AddWithValue("@Plant", objRetDC.PlantName);
                    if (objRetDC.VInDate == DateTime.MinValue)
                        objCmd.Parameters.AddWithValue("@VInDate", DBNull.Value);
                    else
                    objCmd.Parameters.AddWithValue("@VInDate", objRetDC.VInDate);
                    
                    if (objRetDC.VInTime == DateTime.MinValue)
                        objCmd.Parameters.AddWithValue("@VInTime", DBNull.Value);
                    else
                        objCmd.Parameters.AddWithValue("@VInTime", objRetDC.VInTime);
                    
                    if (objRetDC.VOutDate == DateTime.MinValue)
                        objCmd.Parameters.AddWithValue("@VOutDate", DBNull.Value);
                    else
                        objCmd.Parameters.AddWithValue("@VOutDate", objRetDC.VOutDate);

                    if (objRetDC.VOutTime == DateTime.MinValue)
                        objCmd.Parameters.AddWithValue("@VOutTime", DBNull.Value);
                    else
                        objCmd.Parameters.AddWithValue("@VOutTime", objRetDC.VOutTime);

                    if (objRetDC.IsNew)
                    {
                        objCmd.Parameters.AddWithValue("@StDate", DateTime.Now);
                        objCmd.Parameters.AddWithValue("@CrBy", currentUser.LoginName);
                        //objRetDC.DBID = General.GenerateDBID(Conn, "RETURNABLEDC");
                        objRetDC.DBID = General.GenerateDBID("SEQDCID", Conn);
                        objRetDC.EntryNo = objRetDC.DBID;
                    }
                    objCmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                    objCmd.Parameters.AddWithValue("@ModBy", currentUser.LoginName);
                    objCmd.Parameters.AddWithValue("@MachineName", General.GetMachineName());
                    objCmd.Parameters.AddWithValue("@dbID", objRetDC.DBID);
                    objCmd.Parameters.AddWithValue("@EntryNo", objRetDC.EntryNo);

                    if (Conn.State != ConnectionState.Open)
                    {
                        Conn.Open();
                    }
                    result = objCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
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
                ObjDelCmd.CommandText = "DELETE FROM RETURNABLEDC WHERE DBID = @dbID";
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
        /// Returns Party list from ReturnableDC Table.
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPartys()
        {
            string strQry;
            DataTable dTable = null;

            strQry = "SELECT DISTINCT PARTYNAME " +
                " FROM RETURNABLEDC " +
                " ORDER BY PARTYNAME ";

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
        /// Returns VehicleNo list from ReturnableDC Table.
        /// </summary>
        /// <returns></returns>
        public static DataTable GetVehicles()
        {
            string strQry;
            DataTable dTable = null;

            strQry = "SELECT DISTINCT VEHICLENO " +
                " FROM RETURNABLEDC " +
                " ORDER BY VEHICLENO ";

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

        public static long GetEntryNo()
        {
            long id;
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                using (SqlCommand objCmd = Conn.CreateCommand())
                {
                    try
                    {
                        objCmd.CommandType = CommandType.Text;
                        objCmd.CommandText = "select max(ENTRYNO) ENTRYNO " +
                            " FROM RETURNABLEDC";

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
            }
            return id;
        }

        public static DataTable GetDCReport(string entryType, DateTime FromDate, DateTime ToDate, string partyName)
        {
            DataTable objList = null;
            string strSql = "SELECT RETURNABLEDC.ENTRYNO, RETURNABLEDC.ENTRYDATE, RETURNABLEDC.ENTRYTYPE, " +
                " RETURNABLEDC.PARTYNAME, RETURNABLEDC.DCNO, RETURNABLEDC.DCDATE, RETURNABLEDC.VINDATE, " +
                " RETURNABLEDC.VINTIME, RETURNABLEDC.VOUTDATE, RETURNABLEDC.VOUTTIME, RETURNABLEDCDETAIL.ITEMCODE, " +
                " RETURNABLEDCDETAIL.QTY, RETURNABLEDCDETAIL.UNIT " +
                " FROM RETURNABLEDC INNER JOIN RETURNABLEDCDETAIL ON (RETURNABLEDC.ENTRYNO = RETURNABLEDCDETAIL.ENTRYNO) " +
                " AND (RETURNABLEDC.ENTRYDATE = RETURNABLEDCDETAIL.ENTRYDATE) " +
                " WHERE RETURNABLEDC.ENTRYTYPE = @entryType " +
                " AND RETURNABLEDC.ENTRYDATE BETWEEN @FromDate and @ToDate ";

            if (partyName != "ALL")
            {
                strSql += " AND RETURNABLEDC.PARTYNAME = @PartyName ";
            }
            strSql += " ORDER BY ENTRYDATE ASC, ENTRYNO ASC";

            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    objCmd.Connection = Conn;
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = strSql;
                    objCmd.Parameters.AddWithValue("@entryType", entryType);
                    objCmd.Parameters.AddWithValue("@FromDate", FromDate);
                    objCmd.Parameters.AddWithValue("@ToDate", ToDate);

                    if (partyName != "ALL")
                    {
                        objCmd.Parameters.AddWithValue("@PartyName", partyName);
                    }

                    if (Conn.State != ConnectionState.Open)
                    {
                        Conn.Open();
                    }

                    SqlDataAdapter da = new SqlDataAdapter(objCmd);
                    da.Fill(objList);
                }
            }
            return objList;
        }
    }
}
