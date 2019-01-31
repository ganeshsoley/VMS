using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EntityObject;

namespace DAL
{
   public class AppointmentDAL
   {
       #region Private Method(s)
       /// <summary>
       /// Fills values fetched from Database to Object objAppont.
       /// </summary>
       /// <param name="myDataRec">Record Object containing data values.</param>
       /// <returns>Returns object ObjAppoint containing Data values from Database.</returns>
       private static Appointment FillDataRecord(IDataRecord myDataRec)
       {
            Appointment objAppoint = new Appointment();
            objAppoint.IsLoading = true;
            objAppoint.DBID = Convert.ToInt32(myDataRec["DBID"]);
            objAppoint.EntryNo = Convert.ToInt32(myDataRec["ENTRYNO"]);
            objAppoint.EntryDate = Convert.ToDateTime(myDataRec["ENTRYDATE"]);
            objAppoint.AppointmentNo  = Convert.ToString(myDataRec["APPOINTMENTNO"]);
            objAppoint.AppointmentDate = Convert.ToDateTime(myDataRec["APPOINTMENTDATE"]);
            objAppoint.ScheduleTime = Convert.ToDateTime(myDataRec["SCHEDULEDTIME"]);
            if (!myDataRec.IsDBNull(myDataRec.GetOrdinal("VISITORID")))
                objAppoint.VisitorID = Convert.ToInt32(myDataRec["VISITORID"]);
            objAppoint.Name = Convert.ToString(myDataRec["NAME"]);
            objAppoint.ContactNo = Convert.ToString(myDataRec["CONTACTNO"]);
            objAppoint.EmployeeID = Convert.ToInt32(myDataRec["EMPLOYEEID"]);
            objAppoint.EmpName = Convert.ToString(myDataRec["INITIALS"]);
            objAppoint.EmpDept = Convert.ToString(myDataRec["DEPTNAME"]);
            objAppoint.Company = Convert.ToString(myDataRec["COMPANY"]);

            objAppoint.IsNew = false;
            objAppoint.IsEdited = false;
            objAppoint.IsDeleted = false;
            objAppoint.IsLoading = false;

            return objAppoint;
       } 
       #endregion

       #region Public Method(s)
       /// <summary>
       /// This method retrieves "Appointment" Record, which is retrieved from Database.
       /// </summary>
       /// <param name="dbid">Unique ID value based on which Record will be fetched from Database.</param>
       /// <returns>Object "Appointment" containing Data Values.</returns>
       public static Appointment GetItem(int dbid)
       {
           
           Appointment objAppoint = null; 
           using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
           {
               using (SqlCommand objCmd = new SqlCommand())
               {
                   try
                   {
                       objCmd.Connection = Conn;
                       objCmd.CommandType = CommandType.Text;
                       objCmd.CommandText = "SELECT AP.*, EMP.INITIALS, DEPT.DEPTNAME " +
                           " FROM APPOINTMENTMASTER AP, EMPMAST EMP, DEPTMAST DEPT " +
                           " WHERE AP.DBID = @DBID " +
                           " AND EMP.DBID = AP.EMPLOYEEID " +
                           " AND DEPT.DBID = EMP.DEPT ";
                       objCmd.Parameters.AddWithValue("@DBID", dbid);

                       if (Conn.State != ConnectionState.Open)
                       {
                           Conn.Open();
                       }

                       SqlDataReader oReader = objCmd.ExecuteReader();
                       if (oReader.Read())
                       {
                           objAppoint = FillDataRecord(oReader);
                           objAppoint.IsNew = false;
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
           return objAppoint;
       }

       /// <summary>
       /// This method provides List of Appointments available in Database.
       /// </summary>
       /// <param name="strWhere">Specifies condition for retrieving records.</param>
       /// <returns>Collection of Appointment Objects.</returns>
       public static AppointmentList GetList(string strWhere, string strAppointmentNo, string strName, bool flgClose)
       {
           AppointmentList objList = null;
            //string strSql = "Select * from APPOINTMENTMASTER ";
            string strSql = "SELECT AP.*, EMP.INITIALS, DEPT.DEPTNAME " +
                " FROM APPOINTMENTMASTER AP, EMPMAST EMP, DEPTMAST DEPT " +
                " WHERE EMP.DBID = AP.EMPLOYEEID " +
                " AND DEPT.DBID = EMP.DEPT " +
                " and AP.APMTCLOSE = @ApmtClose ";

           if (strAppointmentNo != string.Empty & strName == string.Empty)
           {
               strSql += " AND APPOINTMENTNO = '" + strAppointmentNo + "'";
           }
           else if (strAppointmentNo == string.Empty & strName != string.Empty)
           {
               strSql += " AND NAME = '" + strName + "'";
           }
           else if ((strAppointmentNo != string.Empty) & (strName != string.Empty))
           {
               strSql += " AND APPOINTMENTNO = '" + strAppointmentNo +
                      "' AND NAME = '" + strName + "'";
           }

           if (strWhere != string.Empty & strWhere != null)
               strSql = strSql + " AND " + strWhere;
           strSql += " ORDER BY Entrydate DESC, ENTRYNO DESC";

           using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
           {
               using (SqlCommand objCmd = new SqlCommand())
               {
                   objCmd.Connection = Conn;
                   objCmd.CommandType = CommandType.Text;
                   objCmd.CommandText = strSql;
                    objCmd.Parameters.AddWithValue("@ApmtClose", flgClose);

                   if (Conn.State != ConnectionState.Open)
                   {
                       Conn.Open();
                   }

                   using (SqlDataReader oReader = objCmd.ExecuteReader())
                   {
                       if (oReader.HasRows)
                       {
                           objList = new AppointmentList();
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
       /// <param name="objAppoint">Object containing Data values to be saved.</param>
       /// <returns>Boolean value True if Record is saved successfully
       /// otherwise returns False indicating Record is not saved.</returns>
       public static bool Save(Appointment objAppoint, User currentUser)
       {
           int result = 0;
           string mDate;
           DateTime dt =Convert.ToDateTime(DateTime.Now.ToShortDateString()); 
           string mdt;
           long mNo;
           long mDbid;
           UserCompany CurrentCompany = new UserCompany();
           using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
           {
               string strSaveQry;
               if (objAppoint.IsNew)
               {
                   strSaveQry = "INSERT INTO APPOINTMENTMASTER(DBID,ENTRYNO,ENTRYDATE,APPOINTMENTNO," +
                       " APPOINTMENTDATE,SCHEDULEDTIME, NAME, COMPANY, CONTACTNO, EMPLOYEEID, " +
                       " ST_DATE, MODIFY_DATE, CRBY, MODBY, MACHINENAME, VISITORID, APMTCLOSE) " +
                       "VALUES (@dbId, @entryNo, @entryDate, @appointmentNo, @appointmentDate," +
                       " @scheduleTime,@Name,@Company, @ContactNo, @employeeID,  " +
                       " @STDate, @ModifyDate, @CrBy, @ModBy, @MachineName, @VisitorID, @ApmtClose)";
               }
               else
               {
                   strSaveQry = "UPDATE APPOINTMENTMASTER " +
                       "SET ENTRYNO =@entryNo, ENTRYDATE =@entryDate , APPOINTMENTNO =@appointmentNo , " +
                       " APPOINTMENTDATE = @appointmentDate, SCHEDULEDTIME = @scheduleTime, NAME = @Name, " +
                       " COMPANY = @Company, CONTACTNO = @ContactNo, EMPLOYEEID = @employeeID, " +
                       " MODIFY_DATE = @ModifyDate, MODBY = @ModBy, MACHINENAME = @MachineName, " +
                       " VISITORID = @VisitorID, APMTCLOSE = @ApmtClose " +
                       " WHERE DBID = @dbId";
               }

               try
               {
                   SqlCommand objCmd = Conn.CreateCommand();
                   objCmd.CommandType = CommandType.Text;
                   objCmd.CommandText = strSaveQry;

                    objCmd.Parameters.AddWithValue("@AppointmentDate", objAppoint.AppointmentDate);
                    objCmd.Parameters.AddWithValue("@ScheduleTime", objAppoint.ScheduleTime);
                    objCmd.Parameters.AddWithValue("@VisitorID", objAppoint.VisitorID);
                    objCmd.Parameters.AddWithValue("@Name", objAppoint.Name);
                    objCmd.Parameters.AddWithValue("@Company", objAppoint.Company);
                    objCmd.Parameters.AddWithValue("@ContactNo", objAppoint.ContactNo);
                    objCmd.Parameters.AddWithValue("@EmployeeID", objAppoint.EmployeeID);

                   if (objAppoint.IsNew)
                   {
                       objCmd.Parameters.AddWithValue("@StDate", DateTime.Now);
                       objCmd.Parameters.AddWithValue("@CrBy", currentUser.LoginName);
                       objAppoint.EntryDate = dt;

                        //objAppoint.DBID = General.GenerateDBID(Conn, "APPOINTMENTMASTER");
                        objAppoint.DBID = General.GenerateDBID("SEQAPPOINTMENTID", Conn);
                       objAppoint.EntryNo = objAppoint.DBID;

                       mDbid = objAppoint.DBID;
                       mNo = 4 - (Convert.ToString(mDbid).Trim().Length);
                       mdt = "";
                       if (mNo == 3) { mdt = "000" + "" + mDbid; }
                       if (mNo == 2) { mdt = "00" + "" + mDbid; }
                       if (mNo == 1) { mdt = "0" + "" + mDbid; }
                       mDate = String.Format("{0:yyMMdd}", dt) + "" + mdt;
                       objAppoint.AppointmentNo = mDate;
                       
                   }
                   objCmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now);
                   objCmd.Parameters.AddWithValue("@ModBy", currentUser.LoginName);
                   objCmd.Parameters.AddWithValue("@MachineName", General.GetMachineName());
                   objCmd.Parameters.AddWithValue("@dbID", objAppoint.DBID);
                    objCmd.Parameters.AddWithValue("@EntryDate", objAppoint.EntryDate);
                    objCmd.Parameters.AddWithValue("@entryNo", objAppoint.EntryNo);
                    objCmd.Parameters.AddWithValue("@AppointmentNo", objAppoint.AppointmentNo);
                    objCmd.Parameters.AddWithValue("@ApmtClose", objAppoint.ApmtClose);

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
               ObjDelCmd.CommandText = "DELETE FROM APPOINTMENTMASTER WHERE DBID = @dbID";
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
       /// <param name="objAppoint">Object Containing New Data Values.</param>
       /// <returns>Boolean value True if Current Record already exists
       /// otherwise returns False indicating current Record Does not exist.</returns>
       public static bool IsAppointExist(Appointment objAppoint)
       {
           bool IsRecordExist = false;
           using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
           {
               try
               {
                   SqlCommand objCmd = Conn.CreateCommand();
                   objCmd.CommandType = CommandType.Text;
                   objCmd.CommandText = "SELECT DBID FROM APPOINTMENTMASTER " +
                       " WHERE NAME = @mName AND APPOINTMENTDATE = @mDate " +
                       " AND DBID <> @dbID ";

                   objCmd.Parameters.AddWithValue("@mName", objAppoint.Name);
                    objCmd.Parameters.AddWithValue("@mDate", objAppoint.AppointmentDate);
                    objCmd.Parameters.AddWithValue("@dbID", objAppoint.DBID);

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

       public static DataTable GetAppointments()
       {
           string strQry;
           DataTable dTable = null;

           strQry = "SELECT APPOINTMENTNO " +
               " FROM APPOINTMENTMASTER " +
               " ORDER BY APPOINTMENTNO ";

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

       public static DataTable GetNames()
       {
           string strQry;
           DataTable dTable = null;

           strQry = "SELECT DISTINCT NAME " +
               " FROM APPOINTMENTMASTER " +
               " ORDER BY NAME ";

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
