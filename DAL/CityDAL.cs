using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EntityObject;

namespace DAL
{
   public class CityDAL
   {
       #region Private Method(s)
       /// <summary>
       /// Fills values fetched from Database to Object objCity.
       /// </summary>
       /// <param name="myDataRec">Record Object containing data values.</param>
       /// <returns>Returns object ObjCity containing Data values from Database.</returns>
       private static City FillDataRecord(IDataRecord myDataRec)
       {
           City objCity = new City();
           objCity.IsLoading = true;
           objCity.DBID = Convert.ToInt32(myDataRec["DBID"]);
           objCity.mCity = Convert.ToString(myDataRec["CITY"]);
           
           objCity.IsNew = false;
           objCity.IsEdited = false;
           objCity.IsDeleted = false;
           objCity.IsLoading = false;

           return objCity;
               
       }
       #endregion

       #region Public Method(s)
       /// <summary>
       /// This method retrieves "City" Record, which is retrieved from Database.
       /// </summary>
       /// <param name="dbid">Unique ID value based on which Record will be fetched from Database.</param>
       /// <returns>Object "City" containing Data Values.</returns>
       public static City GetItem(int dbid)
       {
           
           City objCity = null; 
           using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
           {
               using (SqlCommand objCmd = new SqlCommand())
               {
                   try
                   {
                       objCmd.Connection = Conn;
                       objCmd.CommandType = CommandType.Text;
                       objCmd.CommandText = "SELECT * FROM CITYMASTER " +
                           " WHERE DBID = @DBID";
                       objCmd.Parameters.AddWithValue("@DBID", dbid);

                       if (Conn.State != ConnectionState.Open)
                       {
                           Conn.Open();
                       }

                       SqlDataReader oReader = objCmd.ExecuteReader();
                       if (oReader.Read())
                       {
                           objCity = FillDataRecord(oReader);
                           objCity.IsNew = false;
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
           return objCity;
       }
       
       /// <summary>
       /// This method provides List of Cities available in Database.
       /// </summary>
       /// <param name="strWhere">Specifies condition for retrieving records.</param>
       /// <returns>Collection of City Objects.</returns>
       public static CityList GetList(string strWhere)
       {
           
           CityList objList = null; 
           
           string strSql = "Select * from CITYMASTER ";

           if (strWhere != string.Empty)
               strSql = strSql + " WHERE " + strWhere;
           strSql += " ORDER BY CITY";

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
                           objList = new CityList();
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
       public static bool Save(City objCity)
       {
           int result = 0;
           UserCompany CurrentCompany = new UserCompany();
           using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
           {
               string strSaveQry;
               if (objCity.IsNew)
               {
                   strSaveQry = "INSERT INTO CITYMASTER(DBID, CITY, " +
                       " ST_DATE, MODIFY_DATE, CRBY, MODBY, MACHINENAME) " +
                       "VALUES (@dbId, @City, " +
                       " @STDate, @ModifyDate, @CrBy, @ModBy, @MachineName)";
               }
               else
               {
                   strSaveQry = "UPDATE CITYMASTER " +
                       "SET CITY = @CITY, MODIFY_DATE = @ModifyDate, " +
                       "MODBY = @ModBy, MACHINENAME = @MachineName " +
                       "WHERE DBID = @dbId";
               }

               try
               {
                   SqlCommand objCmd = Conn.CreateCommand();
                   objCmd.CommandType = CommandType.Text;
                   objCmd.CommandText = strSaveQry;

                   objCmd.Parameters.AddWithValue("@CITY", objCity.mCity);
                   

                   if (objCity.IsNew)
                   {
                       objCmd.Parameters.AddWithValue("@StDate", DateTime.Now);
                       objCmd.Parameters.AddWithValue("@CrBy", CurrentCompany.m_UserName);
                       objCity.DBID = General.GenerateDBID(Conn, "CITYMASTER");
                   }
                   objCmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                   objCmd.Parameters.AddWithValue("@ModBy", CurrentCompany.m_UserName);
                   objCmd.Parameters.AddWithValue("@MachineName", General.GetMachineName());
                   objCmd.Parameters.AddWithValue("@dbID", objCity.DBID);

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
               ObjDelCmd.CommandText = "DELETE FROM CITYMASTER WHERE DBID = @dbID";
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
       /// This method Checks whether Current City already exists in Database or not.
       /// </summary>
       /// <param name="objCity">Object Containing New Data Values.</param>
       /// <returns>Boolean value True if Current Record already exists
       /// otherwise returns False indicating current Record Does not exist.</returns>
       public static bool IsCityExist(City objCity)
       {
           bool IsRecordExist = false;
           using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
           {
               try
               {
                   SqlCommand objCmd = Conn.CreateCommand();
                   objCmd.CommandType = CommandType.Text;
                   objCmd.CommandText = "SELECT DBID FROM CITYMASTER " +
                       " WHERE City = @mCITY " +
                       " AND DBID <> @dbID ";

                   objCmd.Parameters.AddWithValue("@mCITY", objCity.mCity);
                   objCmd.Parameters.AddWithValue("@dbID", objCity.DBID);

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
