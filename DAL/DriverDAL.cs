using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EntityObject;

namespace DAL
{
   public class DriverDAL
   {
       #region Private Method(s)
       /// <summary>
       /// Fills values fetched from Database to Object objDriver.
       /// </summary>
       /// <param name="myDataRec">Record Object containing data values.</param>
       /// <returns>Returns object ObjDriver containing Data values from Database.</returns>
       private static Driver FillDataRecord(IDataRecord myDataRec)
       {
           
           Driver objDriver = new Driver(); 
           objDriver.IsLoading = true;
           objDriver.DBID = Convert.ToInt32(myDataRec["DBID"]);
           objDriver.Name = Convert.ToString(myDataRec["NAME"]);
           objDriver.LicenceNo = Convert.ToString(myDataRec["LICENSENO"]);
           objDriver.IsActive = Convert.ToBoolean(myDataRec["ISACTIVE"]);

           objDriver.IsNew = false;
           objDriver.IsEdited = false;
           objDriver.IsDeleted = false;
           objDriver.IsLoading = false;

           return objDriver;
       }
       #endregion

       #region Public Method(s)
       /// <summary>
       /// This method retrieves "Driver" Record, which is retrieved from Database.
       /// </summary>
       /// <param name="dbid">Unique ID value based on which Record will be fetched from Database.</param>
       /// <returns>Object "Driver" containing Data Values.</returns>
       public static Driver GetItem(int dbid)
       {
           Driver objDriver = null;
           using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
           {
               using (SqlCommand objCmd = new SqlCommand())
               {
                   try
                   {
                       objCmd.Connection = Conn;
                       objCmd.CommandType = CommandType.Text;
                       objCmd.CommandText = "SELECT * FROM DRIVERMASTER " +
                           " WHERE DBID = @DBID";
                       objCmd.Parameters.AddWithValue("@DBID", dbid);

                       if (Conn.State != ConnectionState.Open)
                       {
                           Conn.Open();
                       }

                       SqlDataReader oReader = objCmd.ExecuteReader();
                       if (oReader.Read())
                       {
                           objDriver = FillDataRecord(oReader);
                           objDriver.IsNew = false;
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
           return objDriver;
       }

       /// <summary>
       /// This method provides List of Drivers available in Database.
       /// </summary>
       /// <param name="strWhere">Specifies condition for retrieving records.</param>
       /// <returns>Collection of Driver Objects.</returns>
       public static DriverList GetList(string strWhere)
       {
           DriverList objList = null;
           string strSql = "SELECT * FROM DRIVERMASTER";

           if (strWhere != string.Empty)
               strSql = strSql + " WHERE " + strWhere;
           strSql += " ORDER BY NAME";

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
                           objList = new DriverList();
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
       /// <param name="objDriver">Object containing Data values to be saved.</param>
       /// <returns>Boolean value True if Record is saved successfully
       /// otherwise returns False indicating Record is not saved.</returns>
       public static bool Save(Driver objDriver, User objUser)
       {
           int result = 0;
           UserCompany CurrentCompany = new UserCompany();
           using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
           {
               string strSaveQry;
               if (objDriver.IsNew)
               {
                   strSaveQry = "INSERT INTO DRIVERMASTER(DBID, NAME, LICENSENO, ISACTIVE, " +
                       " ST_DATE, MODIFY_DATE, CRBY, MODBY, MACHINENAME) " +
                       "VALUES (@dbId, @Name, @LicenseNo, @IsActive, " +
                       " @STDate, @ModifyDate, @CrBy, @ModBy, @MachineName)";
               }
               else
               {
                   strSaveQry = "UPDATE DRIVERMASTER " +
                       "SET NAME = @Name, LICENSENO = @LicenseNo, ISACTIVE = @IsActive, " +
                       "MODIFY_DATE = @ModifyDate, MODBY = @ModBy, MACHINENAME = @MachineName " +
                       "WHERE DBID = @dbId";
               }

               try
               {
                   SqlCommand objCmd = Conn.CreateCommand();
                   objCmd.CommandType = CommandType.Text;
                   objCmd.CommandText = strSaveQry;

                   objCmd.Parameters.AddWithValue("@Name", objDriver.Name);
                   objCmd.Parameters.AddWithValue("@LicenseNo", objDriver.LicenceNo);
                   objCmd.Parameters.AddWithValue("@IsActive", objDriver.IsActive);

                   if (objDriver.IsNew)
                   {
                       objCmd.Parameters.AddWithValue("@StDate", DateTime.Now);
                       objCmd.Parameters.AddWithValue("@CrBy", objUser.LoginName);
                       objDriver.DBID = General.GenerateDBID(Conn, "DRIVERMASTER");
                   }
                   objCmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                   objCmd.Parameters.AddWithValue("@ModBy", objUser.LoginName);
                   objCmd.Parameters.AddWithValue("@MachineName", General.GetMachineName());
                   objCmd.Parameters.AddWithValue("@dbID", objDriver.DBID);

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
               ObjDelCmd.CommandText = "DELETE FROM DRIVERMASTER WHERE DBID = @dbID";
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
       /// <param name="objDriver">Object Containing New Data Values.</param>
       /// <returns>Boolean value True if Current Record already exists
       /// otherwise returns False indicating current Record Does not exist.</returns>
       public static bool IsDriverExist(Driver objDriver)
       {
           bool IsRecordExist = false;
           using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
           {
               try
               {
                   SqlCommand objCmd = Conn.CreateCommand();
                   objCmd.CommandType = CommandType.Text;
                   objCmd.CommandText = "SELECT DBID FROM DRIVERMASTER " +
                       " WHERE NAME = @mName " +
                       " AND DBID <> @dbID ";

                   objCmd.Parameters.AddWithValue("@mName", objDriver.Name);
                   objCmd.Parameters.AddWithValue("@dbID", objDriver.DBID);

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
