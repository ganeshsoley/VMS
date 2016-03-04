using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EntityObject; 

namespace DAL
{
   public class VehicleDAL
   {
       #region Private Method(s)
       /// <summary>
       /// Fills values fetched from Database to Object objVehicle.
       /// </summary>
       /// <param name="myDataRec">Record Object containing data values.</param>
       /// <returns>Returns object ObjVehicle containing Data values from Database.</returns>
       private static Vehicle FillDataRecord(IDataRecord myDataRec)
       {
           Vehicle objVehicle = new Vehicle();
           objVehicle.IsLoading = true;
           objVehicle.Dbid = Convert.ToInt32(myDataRec["DBID"]);
           objVehicle.VehicleNo = Convert.ToString(myDataRec["VEHICLENO"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("VLICENSENO")))
                objVehicle.VLicencseNo = Convert.ToString(myDataRec["VLICENSENO"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("PUCEXPIRY")))
                objVehicle.PUCExpiry = Convert.ToDateTime(myDataRec["PUCEXPIRY"]);
           objVehicle.IsActive = Convert.ToInt16(myDataRec["ISACTIVE"]);

           objVehicle.IsNew = false;
           objVehicle.IsEdited = false;
           objVehicle.IsDeleted = false;
           objVehicle.IsLoading = false;

           return objVehicle;
       }
       #endregion

       #region Public Method(s)
       /// <summary>
       /// This method retrieves "Vehicle" Record, which is retrieved from Database.
       /// </summary>
       /// <param name="dbid">Unique ID value based on which Record will be fetched from Database.</param>
       /// <returns>Object "Vehicle" containing Data Values.</returns>
       public static Vehicle GetItem(int dbid)
       {
           Vehicle objVehicle = null;
           using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
           {
               using (SqlCommand objCmd = new SqlCommand())
               {
                   try
                   {
                       objCmd.Connection = Conn;
                       objCmd.CommandType = CommandType.Text;
                       objCmd.CommandText = "SELECT * FROM VEHICLEMASTER " +
                           " WHERE DBID = @DBID";
                       objCmd.Parameters.AddWithValue("@DBID", dbid);

                       if (Conn.State != ConnectionState.Open)
                       {
                           Conn.Open();
                       }

                       SqlDataReader oReader = objCmd.ExecuteReader();
                       if (oReader.Read())
                       {
                           objVehicle = FillDataRecord(oReader);
                           objVehicle.IsNew = false;
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
           return objVehicle;
       }

       /// <summary>
       /// This method provides List of Vehicle available in Database.
       /// </summary>
       /// <param name="strWhere">Specifies condition for retrieving records.</param>
       /// <returns>Collection of Vehicle Objects.</returns>
       public static VehicleList GetList(string strWhere, bool blnIsActive)
       {
           VehicleList objList = null;
            string strSql = "Select * from VEHICLEMASTER " +
                 " WHERE ISACTIVE = @IsActive";

           if (strWhere != string.Empty)
               strSql = strSql + " AND " + strWhere;
           strSql += " ORDER BY VEHICLENO";

           using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
           {
               using (SqlCommand objCmd = new SqlCommand())
               {
                   objCmd.Connection = Conn;
                   objCmd.CommandType = CommandType.Text;
                   objCmd.CommandText = strSql;
                    objCmd.Parameters.AddWithValue("@IsActive", blnIsActive);

                   if (Conn.State != ConnectionState.Open)
                   {
                       Conn.Open();
                   }

                   using (SqlDataReader oReader = objCmd.ExecuteReader())
                   {
                       if (oReader.HasRows)
                       {
                           objList = new VehicleList();
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
       public static bool Save(Vehicle  objVehicle)
       {
           int result = 0;
           UserCompany CurrentCompany = new UserCompany();
           using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
           {
               string strSaveQry;
               if (objVehicle.IsNew)
               {
                   strSaveQry = "INSERT INTO VEHICLEMASTER(DBID, VEHICLENO, VLICENSENO,PUCEXPIRY,ISACTIVE, " +
                       " ST_DATE, MODIFY_DATE, CRBY, MODBY, MACHINENAME) " +
                       "VALUES (@dbId, @VehicleNo, @VLicencseNo,@PUCExpiry, @IsActive, " +
                       " @STDate, @ModifyDate, @CrBy, @ModBy, @MachineName)";
               }
               else
               {
                   strSaveQry = "UPDATE VEHICLEMASTER " +
                       "SET VEHICLENO = @VehicleNo, VLICENSENO = @VLicencseNo,PUCEXPIRY = @PUCExpiry, ISACTIVE = @IsActive, " +
                       "MODIFY_DATE = @ModifyDate, MODBY = @ModBy, MACHINENAME = @MachineName " +
                       "WHERE DBID = @dbId";
               }

               try
               {
                   SqlCommand objCmd = Conn.CreateCommand();
                   objCmd.CommandType = CommandType.Text;
                   objCmd.CommandText = strSaveQry;

                   objCmd.Parameters.AddWithValue("@VehicleNo", objVehicle.VehicleNo);
                   objCmd.Parameters.AddWithValue("@VLicencseNo", objVehicle.VLicencseNo);
                    if (objVehicle.PUCExpiry != DateTime.MinValue)
                        objCmd.Parameters.AddWithValue("@PUCExpiry", objVehicle.PUCExpiry);
                    else
                        objCmd.Parameters.AddWithValue("@PUCExpiry", DBNull.Value);
                   objCmd.Parameters.AddWithValue("@IsActive", objVehicle.IsActive);

                   if (objVehicle.IsNew)
                   {
                       objCmd.Parameters.AddWithValue("@StDate", DateTime.Now);
                       objCmd.Parameters.AddWithValue("@CrBy", CurrentCompany.m_UserName);
                       objVehicle.Dbid = General.GenerateDBID(Conn, "VEHICLEMASTER");
                   }
                   objCmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                   objCmd.Parameters.AddWithValue("@ModBy", CurrentCompany.m_UserName);
                   objCmd.Parameters.AddWithValue("@MachineName", General.GetMachineName());
                   objCmd.Parameters.AddWithValue("@dbID", objVehicle.Dbid);

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
               ObjDelCmd.CommandText = "DELETE FROM VEHICLEMASTER WHERE DBID = @dbID";
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
       public static bool IsVehicleExist(Vehicle objVehicle)
       {
           bool IsRecordExist = false;
           using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
           {
               try
               {
                   SqlCommand objCmd = Conn.CreateCommand();
                   objCmd.CommandType = CommandType.Text;
                   objCmd.CommandText = "SELECT DBID FROM VEHICLEMASTER " +
                       " WHERE DEPTNAME = @mVEHICLENO " +
                       " AND DBID <> @dbID ";

                   objCmd.Parameters.AddWithValue("@mVEHICLENO", objVehicle.VehicleNo);
                   objCmd.Parameters.AddWithValue("@dbID", objVehicle.Dbid);

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
       #endregion
   }
}
