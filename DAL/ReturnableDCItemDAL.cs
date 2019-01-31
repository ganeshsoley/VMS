using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EntityObject;
using DAL;

namespace DAL
{
    public class ReturnableDCItemDAL
    {
        #region Private Method(s)
        /// <summary>
        /// Fills values fetched from Database to Object objDCItem.
        /// </summary>
        /// <param name="myDataRec">Record Object containing data values.</param>
        /// <returns>Returns object ObjDCItem containing Data values from Database.</returns>
        private static ReturnableDCItem FillDataRecord(IDataRecord myDataRec)
        {
            ReturnableDCItem objDCItem = new ReturnableDCItem();
            objDCItem.IsLoading = true;
            objDCItem.DBID = Convert.ToInt32(myDataRec["DBID"]);
            objDCItem.EntryNo = Convert.ToInt32(myDataRec["ENTRYNO"]);
            objDCItem.EntryDate = Convert.ToDateTime(myDataRec["ENTRYDATE"]);
            objDCItem.EntryType = Convert.ToString(myDataRec["ENTRYTYPE"]);
            objDCItem.DCNo = Convert.ToString(myDataRec["DCNO"]);
            objDCItem.DCDate = Convert.ToDateTime(myDataRec["DCDATE"]);
            objDCItem.ItemCode = Convert.ToString(myDataRec["ITEMCODE"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("ITEMDESCR")))
                objDCItem.ItemDescr = Convert.ToString(myDataRec["ITEMDESCR"]);
            objDCItem.Qty = Convert.ToDecimal(myDataRec["QTY"]);
            objDCItem.Qty2= Convert.ToDecimal(myDataRec["QTY2"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("UNIT")))
                objDCItem.UnitName = Convert.ToString(myDataRec["UNIT"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("REMARK")))
                objDCItem.Remark = Convert.ToString(myDataRec["REMARK"]);
            objDCItem.MasterDBID = Convert.ToInt32(myDataRec["MASTERDBID"]);

            objDCItem.IsNew = false;
            objDCItem.IsEdited = false;
            objDCItem.IsDeleted = false;
            objDCItem.IsLoading = false;

            return objDCItem;
        }
        #endregion

        /// <summary>
        /// This method retrieves "ReturnableDCItem" Record from Database.
        /// </summary>
        /// <param name="dbid">Unique ID value based on which Record will be fetched from Database.</param>
        /// <returns>Object "ReturnableDCItem" containing Data Values.</returns>
        public static ReturnableDCItem GetItem(int dbid)
        {
            ReturnableDCItem objDCItem = null;
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    try
                    {
                        objCmd.Connection = Conn;
                        objCmd.CommandType = CommandType.Text;
                        objCmd.CommandText = "SELECT * FROM RETURNABLEDCDETAIL " +
                            " WHERE DBID = @DBID";
                        objCmd.Parameters.AddWithValue("@DBID", dbid);

                        if (Conn.State != ConnectionState.Open)
                        {
                            Conn.Open();
                        }

                        SqlDataReader oReader = objCmd.ExecuteReader();
                        if (oReader.Read())
                        {
                            objDCItem = FillDataRecord(oReader);
                            objDCItem.IsNew = false;
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
            return objDCItem;
        }

        /// <summary>
        /// This method provides List of Returnable DCs available in Database.
        /// </summary>
        /// <param name="strWhere">Specifies condition for retrieving records.</param>
        /// <returns>Collection of Returnable DC Objects.</returns>
        public static ReturnableDCItemList GetList(long pEntryNo, DateTime pEntryDate, long pMasterDBID)
        {
            ReturnableDCItemList objList = null;
            string strSql = "Select * from ReturnableDCDetail " +
                " WHERE ENTRYNO = @entryNo " +
                " AND ENTRYDATE = @entryDate " +
                " AND MASTERDBID = @masterDBID " +
                " ORDER BY DBID";

            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    objCmd.Connection = Conn;
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = strSql;
                    objCmd.Parameters.AddWithValue("@entryNo", pEntryNo);
                    objCmd.Parameters.AddWithValue("@entryDate", pEntryDate);
                    objCmd.Parameters.AddWithValue("@masterDBID", pMasterDBID);

                    try
                    {
                        if (Conn.State != ConnectionState.Open)
                        {
                            Conn.Open();
                        }

                        using (SqlDataReader oReader = objCmd.ExecuteReader())
                        {
                            if (oReader.HasRows)
                            {
                                objList = new ReturnableDCItemList();
                                while (oReader.Read())
                                {
                                    objList.Add(FillDataRecord(oReader));
                                }
                            }
                            oReader.Close();
                            oReader.Dispose();
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            return objList;
        }

        /// <summary>
        /// This method Saves Record into Database.
        /// </summary>
        /// <param name="objDCItem">Object containing Data values to be saved.</param>
        /// <returns>Boolean value True if Record is saved successfully
        /// otherwise returns False indicating Record is not saved.</returns>
        public static bool Save(ReturnableDCItem objDCItem, User currentUser)
        {
            int result = 0;
            UserCompany CurrentCompany = new UserCompany();
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                string strSaveQry;
                if (objDCItem.IsNew)
                {
                    strSaveQry = "INSERT INTO RETURNABLEDCDETAIL(DBID, ENTRYNO, ENTRYDATE, ENTRYTYPE, " +
                        " DCNO, DCDATE, ITEMCODE, ITEMDESCR, QTY, QTY2, UNIT, REMARK, MASTERDBID, " +
                        " ST_DATE, MODIFY_DATE, CRBY, MODBY, MACHINENAME) " +
                        " VALUES (@dbId, @EntryNo, @EntryDate, @EntryType, @DCNo, @DCDate, " +
                        " @ItemCode, @ItemDescr, @Qty, @Qty2, @Unit, @Remark, @MasterDBID, " +
                        " @STDate, @ModifyDate, @CrBy, @ModBy, @MachineName)";
                }
                else
                {
                    strSaveQry = "UPDATE RETURNABLEDCDETAIL " +
                        " SET ENTRYNO = @EntryNo, ENTRYDATE = @EntryDate, ENTRYTYPE = @EntryType, " +
                        " DCNO = @DCNo, DCDATE = @DCDate, ITEMCODE = @ItemCode, ITEMDESCR = @ItemDescr" +
                        " Qty = @QTY, QTY2 = @Qty2, UNIT = @Unit, REMARK = @Remark, " +
                        " MASTERDBID = @MasterDBID, MODIFY_DATE = @ModifyDate, " +
                        " MODBY = @ModBy, MACHINENAME = @MachineName " +
                        " WHERE DBID = @dbId";
                }

                try
                {
                    SqlCommand objCmd = Conn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = strSaveQry;

                    objCmd.Parameters.AddWithValue("@EntryNo", objDCItem.EntryNo);
                    objCmd.Parameters.AddWithValue("@EntryDate", objDCItem.EntryDate);
                    objCmd.Parameters.AddWithValue("@EntryType", objDCItem.EntryType);
                    objCmd.Parameters.AddWithValue("@DCNo", objDCItem.DCNo);
                    objCmd.Parameters.AddWithValue("@DCDate", objDCItem.DCDate);
                    objCmd.Parameters.AddWithValue("@ItemCode", objDCItem.ItemCode);
                    objCmd.Parameters.AddWithValue("@ItemDescr", objDCItem.ItemDescr);
                    objCmd.Parameters.AddWithValue("@Qty", objDCItem.Qty);
                    objCmd.Parameters.AddWithValue("@Qty2", objDCItem.Qty2);
                    objCmd.Parameters.AddWithValue("@Unit", objDCItem.UnitName);
                    objCmd.Parameters.AddWithValue("@Remark", objDCItem.Remark);

                    if (objDCItem.IsNew)
                    {
                        objCmd.Parameters.AddWithValue("@StDate", DateTime.Now);
                        objCmd.Parameters.AddWithValue("@CrBy", currentUser.LoginName);
                        //objDCItem.DBID = General.GenerateDBID(Conn, "RETURNABLEDCDETAIL");
                        objDCItem.DBID = General.GenerateDBID("SEQDCITEMID", Conn);
                    }
                    objCmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                    objCmd.Parameters.AddWithValue("@ModBy", currentUser.LoginName);
                    objCmd.Parameters.AddWithValue("@MachineName", General.GetMachineName());
                    objCmd.Parameters.AddWithValue("@dbID", objDCItem.DBID);
                    objCmd.Parameters.AddWithValue("@MasterDBID", objDCItem.MasterDBID);

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
                ObjDelCmd.CommandText = "DELETE FROM RETURNABLEDCDETAIL WHERE DBID = @dbID";
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

        public static DataTable GetItems()
        {
            string strQry;
            DataTable dTable = null;

            strQry = "SELECT DISTINCT ITEMCODE " +
                " FROM RETURNABLEDCDETAIL " +
                " ORDER BY ITEMCODE ";

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

        public static DataTable GetUnits()
        {
            string strQry;
            DataTable dTable = null;

            strQry = "SELECT DISTINCT UNIT " +
                " FROM RETURNABLEDCDETAIL " +
                " ORDER BY UNIT ";

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
