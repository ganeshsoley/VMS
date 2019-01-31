using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EntityObject;

namespace DAL
{
   public class VehInOutDAL
   {
       #region Private Method(s)
       /// <summary>
       /// Fills values fetched from Database to Object objVehInOut.
       /// </summary>
       /// <param name="myDataRec">Record Object containing data values.</param>
       /// <returns>Returns object objVehInOut containing Data values from Database.</returns>
       private static VehInOut FillDataRecord(IDataRecord myDataRec)
       {
           VehInOut objVehInOut = new VehInOut();
           objVehInOut.IsLoading = true;
           objVehInOut.Dbid = Convert.ToInt32(myDataRec["DBID"]);
           objVehInOut.EntryNo = Convert.ToInt32(myDataRec["ENTRYNO"]);
           objVehInOut.EntryDate = Convert.ToDateTime(myDataRec["ENTRYDATE"]);
           objVehInOut.Type = Convert.ToInt16(myDataRec["TYPE"]);
           objVehInOut.InOut = Convert.ToInt16(myDataRec["INOUT"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("INDATE")))
                objVehInOut.InDate = Convert.ToDateTime(myDataRec["INDATE"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("INTIME")))
                objVehInOut.InTime = Convert.ToDateTime(myDataRec["INTIME"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("OUTDATE")))
                objVehInOut.OutDate = Convert.ToDateTime(myDataRec["OUTDATE"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("OUTTIME")))
                objVehInOut.OutTime = Convert.ToDateTime(myDataRec["OUTTIME"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("VEHNO")))
                objVehInOut.VehNo = Convert.ToString(myDataRec["VEHNO"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("DRIVERNAME")))
                objVehInOut.DriverName = Convert.ToString(myDataRec["DRIVERNAME"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("CITYNAME")))
                objVehInOut.CityName = Convert.ToString(myDataRec["CITYNAME"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("VENDORIN")))
                objVehInOut.VendorIn = Convert.ToString(myDataRec["VENDORIN"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("VENDOROUT")))
                objVehInOut.VendorOut = Convert.ToString(myDataRec["VENDOROUT"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("PLANT")))
                objVehInOut.Plant = Convert.ToString(myDataRec["PLANT"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("ININVCNT")))
                objVehInOut.InInvCnt = Convert.ToInt32(myDataRec["ININVCNT"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("OUTINVCNT")))
                objVehInOut.OutInvCnt = Convert.ToInt32(myDataRec["OUTINVCNT"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("INCARRYMATERIAL")))
                objVehInOut.InCarryMaterial = Convert.ToString(myDataRec["INCARRYMATERIAL"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("OUTCARRYMATERIAL")))
                objVehInOut.OutCarryMaterial = Convert.ToString(myDataRec["OUTCARRYMATERIAL"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("PUCFLG")))
                objVehInOut.PUCFlg = Convert.ToString(myDataRec["PUCFLG"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("PUCNO")))
                objVehInOut.PUCNo = Convert.ToString(myDataRec["PUCNO"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("RCBOOKFLG")))
                objVehInOut.RCBookFlg   = Convert.ToString(myDataRec["RCBOOKFLG"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("RCBOOKNO")))
                objVehInOut.RCBookNo = Convert.ToString(myDataRec["RCBOOKNO"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("FINANCE")))
                objVehInOut.Finance = Convert.ToString(myDataRec["FINANCE"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("FITNESSFLG")))
                objVehInOut.FitnessFlg  = Convert.ToString(myDataRec["FITNESSFLG"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("FITNESSNO")))
                objVehInOut.FitnessNo  = Convert.ToString(myDataRec["FITNESSNO"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("DRLICFLG")))
                objVehInOut.DrLicFlg = Convert.ToString(myDataRec["DRLICFLG"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("DRLICNO")))
                objVehInOut.DrLicNo = Convert.ToString(myDataRec["DRLICNO"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("PERSONSWITHVEH")))
                objVehInOut.PersonsWithVeh = Convert.ToInt16(myDataRec["PERSONSWITHVEH"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("INREADING")))
                objVehInOut.InReading = Convert.ToInt32(myDataRec["INREADING"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("OUTREADING")))
                objVehInOut.OutReading = Convert.ToInt32(myDataRec["OUTREADING"]);

           objVehInOut.IsNew = false;
           objVehInOut.IsEdited = false;
           objVehInOut.IsDeleted = false;
           objVehInOut.IsLoading = false;

           return objVehInOut;
       } 
       #endregion

       #region Public Methods
       /// <summary>
       /// This method retrieves "VehInOut" Record, which is retrieved from Database.
       /// </summary>
       /// <param name="dbid">Unique ID value based on which Record will be fetched from Database.</param>
       /// <returns>Object "VehInOut" containing Data Values.</returns>
       public static VehInOut GetItem(int dbid)
       {

           VehInOut objVehInOut = null;
           using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
           {
               using (SqlCommand objCmd = new SqlCommand())
               {
                   try
                   {
                       objCmd.Connection = Conn;
                       objCmd.CommandType = CommandType.Text;
                       objCmd.CommandText = "SELECT * FROM VEHINOUTDETAIL WHERE DBID = @DBID";
                       objCmd.Parameters.AddWithValue("@DBID", dbid);

                       if (Conn.State != ConnectionState.Open)
                       {
                           Conn.Open();
                       }

                       SqlDataReader oReader = objCmd.ExecuteReader();
                       if (oReader.Read())
                       {
                           objVehInOut = FillDataRecord(oReader);
                           objVehInOut.IsNew = false;
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
           return objVehInOut;
       }

       /// <summary>
       /// This method provides List of VehInOut available in Database.
       /// </summary>
       /// <param name="strWhere">Specifies condition for retrieving records.</param>
       /// <returns>Collection of VehInOut Objects.</returns>
       public static VehInOutList GetList(string strWhere, DateTime vwDate, bool flgShowAll, int EntryType)
       {
           VehInOutList objList = null;
           string strSql = "SELECT * FROM VEHINOUTDETAIL";

           if (!flgShowAll)
           {
               strSql += " WHERE ENTRYDATE = @entryDate ";

               if (strWhere != string.Empty)
                   strSql = strSql + " AND " + strWhere;
                strSql += " AND TYPE = @mEntryType ";
           }
           else
           {
                if (strWhere != string.Empty)
                {
                    strSql = strSql + " WHERE " + strWhere;
                    strSql += " AND TYPE = @mEntryType ";
                }
                else
                    strSql += " WHERE TYPE = @mEntryType ";
            }
            
           strSql += " ORDER BY ENTRYDATE DESC, ENTRYNO DESC";

           using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
           {
               using (SqlCommand objCmd = new SqlCommand())
               {
                   objCmd.Connection = Conn;
                   objCmd.CommandType = CommandType.Text;
                   objCmd.CommandText = strSql;
                    objCmd.Parameters.AddWithValue("@mEntryType", EntryType);
                    if (!flgShowAll)
                   {
                       objCmd.Parameters.AddWithValue("@entryDate", vwDate.ToString("dd-MMM-yy"));
                   }

                   if (Conn.State != ConnectionState.Open)
                   {
                       Conn.Open();
                   }

                   using (SqlDataReader oReader = objCmd.ExecuteReader())
                   {
                       if (oReader.HasRows)
                       {
                           objList = new VehInOutList();
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
       public static bool Save(VehInOut objVehInOut, User objUser)
       {
           int result = 0;
           string strSaveQry;
           UserCompany CurrentCompany = new UserCompany();
           using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
           {
               //string strSaveQry;

               if (objVehInOut.IsNew)
               {
                    //objCmd.CommandText = "INSERT INTO VEHINOUTDETAIL(DBID,ENTRYNO,ENTRYDATE,TYPE," +
                    //    " INOUT,INDATE,INTIME,OUTDATE,OUTTIME,VEHNO,DRIVERNAME,CITYNAME," +
                    //    " VENDORIN,VENDOROUT,PLANT,ININVCNT,OUTINVCNT,INCARRYMATERIAL, " +
                    //    " OUTCARRYMATERIAL,PUCFLG,PUCNO,RCBOOKFLG,RCBOOKNO,FINANCE, " +
                    //    " FITNESSFLG,FITNESSNO,DRLICFLG,DRLICNO,PERSONSWITHVEH, " +
                    //    " ST_DATE, MODIFY_DATE, CR_BY, MOD_BY, MACHINENAME, INREADING, OUTREADING) " +
                    //    " VALUES (" + objVehInOut.Dbid + "," +
                    //    " " + objVehInOut.EntryNo + "," +
                    //    " '" + objVehInOut.EntryDate.ToString("dd-MMM-yy") + "'," +
                    //    " " + objVehInOut.Type + "," +
                    //    " " + objVehInOut.InOut + "," +
                    //    " '" + objVehInOut.InDate.ToString("dd-MMM-yy") + "'," +
                    //    " '" + objVehInOut.InTime.ToShortTimeString() + "'," +
                    //    " '" + objVehInOut.OutDate.ToString("dd-MMM-yy") + "'," +
                    //    " '" + objVehInOut.OutTime.ToShortTimeString() + "'," +
                    //    " '" + objVehInOut.VehNo + "'," +
                    //    " '" + objVehInOut.DriverName + "'," +
                    //    " '" + objVehInOut.CityName + "'," +
                    //    " '" + objVehInOut.VendorIn + "'," +
                    //    " '" + objVehInOut.VendorOut + "'," +
                    //    " '" + objVehInOut.Plant + "'," +
                    //    " " + objVehInOut.InInvCnt + "," +
                    //    " " + objVehInOut.OutInvCnt + "," +
                    //    " '" + objVehInOut.InCarryMaterial + "'," +
                    //    " '" + objVehInOut.OutCarryMaterial + "'," +
                    //    " '" + objVehInOut.PUCFlg + "'," +
                    //    " '" + objVehInOut.PUCNo + "'," +
                    //    " '" + objVehInOut.RCBookFlg + "'," +
                    //    " '" + objVehInOut.RCBookNo + "'," +
                    //    " '" + objVehInOut.Finance + "'," +
                    //    " '" + objVehInOut.FitnessFlg + "'," +
                    //    " '" + objVehInOut.FitnessNo + "'," +
                    //    " '" + objVehInOut.DrLicFlg + "'," +
                    //    " '" + objVehInOut.DrLicNo + "'," +
                    //    " " + objVehInOut.PersonsWithVeh + "," +
                    //    " '" + DateTime.Now.ToString("dd-MMM-yy") + "'," +
                    //    " '" + DateTime.Now.ToString("dd-MMM-yy") + "'," +
                    //    " '" + CurrentCompany.m_UserName + "'," +
                    //    " '" + CurrentCompany.m_UserName + "'," +
                    //    " '" + System.Environment.MachineName + "'," + objVehInOut.InReading + "," + objVehInOut.OutReading + ")";

                    strSaveQry = "INSERT INTO VEHINOUTDETAIL(DBID, ENTRYNO, ENTRYDATE, TYPE, " +
                        " INOUT, INDATE, INTIME, OUTDATE, OUTTIME, VEHNO, DRIVERNAME, CITYNAME," +
                        " VENDORIN, VENDOROUT, PLANT, ININVCNT, OUTINVCNT, INCARRYMATERIAL, " +
                        " OUTCARRYMATERIAL, PUCFLG, PUCNO, RCBOOKFLG, RCBOOKNO, FINANCE, " +
                        " FITNESSFLG, FITNESSNO, DRLICFLG, DRLICNO, PERSONSWITHVEH, " +
                        " ST_DATE, MODIFY_DATE, CR_BY, MOD_BY, MACHINENAME, INREADING, OUTREADING) " +
                        " VALUES (@DBID, @ENTRYNO, @ENTRYDATE, @TYPE, " +
                        " @INOUT, @INDATE, @INTIME, @OUTDATE, @OUTTIME, @VEHNO, @DRIVERNAME, @CITYNAME, " +
                        " @VENDORIN, @VENDOROUT, @PLANT, @ININVCNT, @OUTINVCNT, @INCARRYMATERIAL," +
                        " @OUTCARRYMATERIAL, @PUCFLG, @PUCNO, @RCBOOKFLG, @RCBOOKNO, @FINANCE, " +
                        " @FITNESSFLG, @FITNESSNO, @DRLICFLG, @DRLICNO, @PERSONSWITHVEH, " +
                        " @STDATE, @ModifyDate, @CrBy, @ModBy, @MachineName, @INREADING, @OUTREADING)";
                }
               else
               {
                    //objCmd.CommandText = "UPDATE VEHINOUTDETAIL " +
                    //   "SET ENTRYNO =" + objVehInOut.EntryNo + "," +
                    //   " ENTRYDATE ='" + objVehInOut.EntryDate.ToString("dd-MMM-yy") + "'," +
                    //   " TYPE  =" + objVehInOut.Type + "," +
                    //   " INOUT = " + objVehInOut.InOut + "," +
                    //   " INDATE ='" + objVehInOut.InDate.ToString("dd-MMM-yy") + "'," +
                    //   " INTIME ='" + objVehInOut.InTime.ToShortTimeString() + "'," +
                    //   " OUTDATE ='" + objVehInOut.OutDate.ToString("dd-MMM-yy") + "'," +
                    //   " OUTTIME ='" + objVehInOut.OutTime.ToShortTimeString() + "'," +
                    //   " VEHNO ='" + objVehInOut.VehNo + "'," +
                    //   " DRIVERNAME = '" + objVehInOut.DriverName + "'," +
                    //   " CITYNAME = '" + objVehInOut.CityName + "'," +
                    //   " VENDORIN = '" + objVehInOut.VendorIn + "'," +
                    //   " VENDOROUT = '" + objVehInOut.VendorIn + "'," +
                    //   " PLANT ='" + objVehInOut.Plant + "'," +
                    //   " ININVCNT = " + objVehInOut.InInvCnt + "," +
                    //   " OUTINVCNT = " + objVehInOut.OutInvCnt + "," +
                    //   " INCARRYMATERIAL = '" + objVehInOut.InCarryMaterial + "'," +
                    //   " OUTCARRYMATERIAL = '" + objVehInOut.OutCarryMaterial + "'," +
                    //   " PUCFLG = '" + objVehInOut.PUCFlg + "'," +
                    //   " PUCNO ='" + objVehInOut.PUCNo + "'," +
                    //   " RCBOOKFLG = '" + objVehInOut.RCBookFlg + "'," +
                    //   " RCBOOKNO = '" + objVehInOut.RCBookNo + "'," +
                    //   " FINANCE = '" + objVehInOut.Finance + "'," +
                    //   " FITNESSFLG ='" + objVehInOut.FitnessFlg + "'," +
                    //   " FITNESSNO = '" + objVehInOut.FitnessNo + "'," +
                    //   " DRLICFLG = '" + objVehInOut.DrLicFlg + "'," +
                    //   " DRLICNO = '" + objVehInOut.DrLicNo + "'," +
                    //   " PERSONSWITHVEH = " + objVehInOut.PersonsWithVeh + "," +
                    //   " MODIFY_DATE = '" + DateTime.Now.ToString("dd-MMM-yy") + "'," +
                    //   " MOD_BY = '" + CurrentCompany.m_UserName + "'," +
                    //   " MACHINENAME ='" + System.Environment.MachineName + "'," +
                    //   " INREADING = " + objVehInOut.InReading + ", " +
                    //   " OUTREADING = " + objVehInOut.OutReading +
                    //   " WHERE DBID = " + objVehInOut.Dbid + "";

                    strSaveQry = "UPDATE VEHINOUTDETAIL " +
                       "SET ENTRYNO = @ENTRYNO , ENTRYDATE = @ENTRYDATE, TYPE = @TYPE, INOUT = @INOUT, " +
                       " INDATE = @INDATE , INTIME = @INTIME , OUTDATE = @OUTDATE , OUTTIME = @OUTTIME , " +
                       " VEHNO = @VEHNO , DRIVERNAME = @DRIVERNAME , CITYNAME = @CITYNAME , VENDORIN = @VENDORIN , " +
                       " VENDOROUT = @VENDOROUT , PLANT = @PLANT, ININVCNT = @ININVCNT, OUTINVCNT = @OUTINVCNT , " +
                       " INCARRYMATERIAL = @INCARRYMATERIAL, OUTCARRYMATERIAL = @OUTCARRYMATERIAL , PUCFLG = @PUCFLG , " +
                       " PUCNO = @PUCNO , RCBOOKFLG = @RCBOOKFLG , RCBOOKNO = @RCBOOKNO, FINANCE = @FINANCE, " +
                       " FITNESSFLG = @FITNESSFLG, FITNESSNO = @FITNESSNO , DRLICFLG = @DRLICFLG , DRLICNO =@DRLICNO , " +
                       " PERSONSWITHVEH = @PERSONSWITHVEH , MODIFY_DATE = @MODIFYDATE, MOD_BY = @MODBY, " +
                       " MACHINENAME = @MACHINENAME, INREADING = @INREADING, OUTREADING = @OUTREADING " +
                       " WHERE DBID = @DBID";
                }

               try
               {
                    SqlCommand objCmd = Conn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = strSaveQry;
                    //objCmd.Parameters.AddWithValue("@ENTRYNO", objVehInOut.EntryNo);
                    //objCmd.Parameters.AddWithValue("@ENTRYDATE", objVehInOut.EntryDate.ToShortDateString());
                    objCmd.Parameters.AddWithValue("@TYPE", objVehInOut.Type);
                    objCmd.Parameters.AddWithValue("@INOUT", objVehInOut.InOut);

                    if (objVehInOut.InDate != DateTime.MinValue)
                    {
                        objCmd.Parameters.AddWithValue("@INDATE", objVehInOut.InDate);
                    }
                    else
                    {
                        objCmd.Parameters.AddWithValue("@INDATE", DBNull.Value);
                    }
                    if (objVehInOut.InTime != DateTime.MinValue)
                    {
                        objCmd.Parameters.AddWithValue("@INTIME", objVehInOut.InTime);
                    }
                    else
                    {
                        objCmd.Parameters.AddWithValue("@INTIME", DBNull.Value);
                    }
                    if (objVehInOut.OutDate != DateTime.MinValue)
                    {
                        objCmd.Parameters.AddWithValue("@OUTDATE", objVehInOut.OutDate);
                    }
                    else
                    {
                        objCmd.Parameters.AddWithValue("@OUTDATE", DBNull.Value);
                    }
                    if(objVehInOut.OutTime != DateTime.MinValue)
                    { 
                        objCmd.Parameters.AddWithValue("@OUTTIME", objVehInOut.OutTime);
                    }
                    else
                    {
                        objCmd.Parameters.AddWithValue("@OUTTIME", DBNull.Value);
                    }

                    objCmd.Parameters.AddWithValue("@VEHNO", objVehInOut.VehNo);
                    objCmd.Parameters.AddWithValue("@DRIVERNAME", objVehInOut.DriverName);
                    objCmd.Parameters.AddWithValue("@CITYNAME", objVehInOut.CityName);
                    objCmd.Parameters.AddWithValue("@VENDORIN", objVehInOut.VendorIn);
                    objCmd.Parameters.AddWithValue("@VENDOROUT", objVehInOut.VendorOut);
                    objCmd.Parameters.AddWithValue("@PLANT", objVehInOut.Plant);
                    objCmd.Parameters.AddWithValue("@ININVCNT", objVehInOut.InInvCnt);
                    objCmd.Parameters.AddWithValue("@OUTINVCNT", objVehInOut.OutInvCnt);
                    objCmd.Parameters.AddWithValue("@INCARRYMATERIAL", objVehInOut.InCarryMaterial);
                    objCmd.Parameters.AddWithValue("@OUTCARRYMATERIAL", objVehInOut.OutCarryMaterial);
                    objCmd.Parameters.AddWithValue("@PUCFLG", objVehInOut.PUCFlg);
                    objCmd.Parameters.AddWithValue("@PUCNO", objVehInOut.PUCNo);
                    objCmd.Parameters.AddWithValue("@RCBOOKFLG", objVehInOut.RCBookFlg);
                    objCmd.Parameters.AddWithValue("@RCBOOKNO", objVehInOut.RCBookNo);
                    objCmd.Parameters.AddWithValue("@FINANCE", objVehInOut.Finance);
                    objCmd.Parameters.AddWithValue("@FITNESSFLG", objVehInOut.FitnessFlg);
                    objCmd.Parameters.AddWithValue("@FITNESSNO", objVehInOut.FitnessNo);
                    objCmd.Parameters.AddWithValue("@DRLICFLG", objVehInOut.DrLicFlg);
                    objCmd.Parameters.AddWithValue("@DRLICNO", objVehInOut.DrLicNo);
                    objCmd.Parameters.AddWithValue("@PERSONSWITHVEH", objVehInOut.PersonsWithVeh);

                    if (objVehInOut.IsNew)
                    {
                        objVehInOut.EntryDate = DateTime.Now.Date;
                        //objVehInOut.Dbid = General.GenerateDBID(Conn, "VEHINOUTDETAIL");
                        objVehInOut.Dbid = General.GenerateDBID("SEQVEHINOUTID", Conn);
                        objVehInOut.EntryNo = objVehInOut.Dbid;

                        objCmd.Parameters.AddWithValue("@STDATE", DateTime.Now);
                        objCmd.Parameters.AddWithValue("@CRBY", objUser.LoginName); //CurrentCompany.m_UserName
                    }
                    objCmd.Parameters.AddWithValue("@DBID", objVehInOut.Dbid);
                    objCmd.Parameters.AddWithValue("@ENTRYNO", objVehInOut.EntryNo);
                    objCmd.Parameters.AddWithValue("@ENTRYDATE", objVehInOut.EntryDate);
                    objCmd.Parameters.AddWithValue("@MODIFYDATE", DateTime.Now);
                    objCmd.Parameters.AddWithValue("@MODBY", objUser.LoginName);  //CurrentCompany.m_UserName
                    objCmd.Parameters.AddWithValue("@MACHINENAME", General.GetMachineName());
                    objCmd.Parameters.AddWithValue("@INREADING", objVehInOut.InReading);
                    objCmd.Parameters.AddWithValue("@OUTREADING", objVehInOut.OutReading);

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
               finally
               {
                   Conn.Close();
               }
               Conn.Dispose();
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
               ObjDelCmd.CommandText = "DELETE FROM VEHINOUTDETAIL WHERE DBID = @dbID";
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
       public static bool IsVehInOutExist(VehInOut objVehInOut)
       {
           bool IsRecordExist = false;
           using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
           {
               try
               {
                   SqlCommand objCmd = Conn.CreateCommand();
                   objCmd.CommandType = CommandType.Text;
                   objCmd.CommandText = "SELECT DBID FROM VEHINOUTDETAIL " +
                       " WHERE DBID <> @dbID ";

                   //objCmd.Parameters.AddWithValue("@mName", objVehInOut.Name);
                   objCmd.Parameters.AddWithValue("@dbID", objVehInOut.Dbid);

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

       public static DataTable GetDrivers()
       {
           string strQry;
           DataTable dTable = null;

           strQry = "SELECT DISTINCT DRIVERNAME " +
               " FROM VEHINOUTDETAIL " +
               " Order by DRIVERNAME ";

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
       public static DataTable GetVehicles()
       {
           string strQry;
           DataTable dTable = null;

           strQry = "SELECT DISTINCT VEHNO " +
               " FROM VEHINOUTDETAIL " +
               " Order by VEHNO ";

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
       public static DataTable GetInVendors()
       {
           string strQry;
           DataTable dTable = null;

           strQry = "SELECT DISTINCT VENDORIN " +
               " FROM VEHINOUTDETAIL " +
               " Order by VENDORIN ";

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
       public static DataTable GetOutVendors()
       {
           string strQry;
           DataTable dTable = null;

           strQry = "SELECT DISTINCT VENDOROUT " +
               " FROM VEHINOUTDETAIL " +
               " Order by VENDOROUT ";

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
       public static DataTable GetCities()
       {
           string strQry;
           DataTable dTable = null;

           strQry = "SELECT DISTINCT CITYNAME " +
               " FROM VEHINOUTDETAIL " +
               " Order by CITYNAME ";

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

 
       #endregion
   }
}
