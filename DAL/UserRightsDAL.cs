using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EntityObject;

namespace DAL
{
    public class UserRightsDAL
    {
        #region Private Method(s)
        /// <summary>
        /// Fills values fetched from Database to Object objUserRights.
        /// </summary>
        /// <param name="myDataRec">Record Object containing data values.</param>
        /// <returns>Returns object ObjUserRights containing Data values from Database.</returns>
        private static UserRights FillDataRecord(IDataRecord myDataRec)
        {
            UserRights objRights = new UserRights();

            objRights.IsLoading = true;

            objRights.UserID = Convert.ToInt32(myDataRec["USERID"]);
            objRights.MenuID = Convert.ToString(myDataRec["MENUID"]);
            objRights.FormName = Convert.ToString(myDataRec["FORMNAME"]);

            objRights.IsNew = false;
            objRights.IsEdited = false;
            objRights.IsDeleted = false;
            objRights.IsLoading = false;

            return objRights;
        }
        #endregion

        public static DataTable GetUserRights(long UserID)
        {
            //UserRightsList objList = null;
            DataTable dTable = null;

            string strSql = "Select * from USERRIGHTS " +
                " WHERE USERID = @mUserID " +
                " ORDER BY MENUID ";

            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    objCmd.Connection = Conn;
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = strSql;
                    objCmd.Parameters.AddWithValue("mUserID", UserID);

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
            }
            return dTable;
        }

        public static DataTable GetUserFormRights(long UserID, string strMenu)
        {
            //UserRightsList objList = null;
            DataTable dTable = null;

            string strSql = "Select * from USERRIGHTS " +
                " WHERE USERID = @mUserID " +
                " AND MENUID LIKE @mMenu " +
                 " ORDER BY MENUID ";

            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    objCmd.Connection = Conn;
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = strSql;
                    objCmd.Parameters.AddWithValue("mUserID", UserID);
                    objCmd.Parameters.AddWithValue("mMenu", strMenu + "%");

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
            }
            return dTable;
        }

        public static bool Save(UserRights objUserRights)
        {
            int result = 0;
            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                string strSaveQry = "";
                //if (objList.IsNew)
                //{
                    strSaveQry = "INSERT INTO USERRIGHTS (USERID, MENUID, FORMNAME) " +
                        " VALUES (@UserID, @MenuID, @FormName) ";
                //}
                //else
                //{
                    //strSaveQry = "UPDATE USERRIGHTS " +
                    //    " SET USERID = @UserID, MENUID = @MenuID, FORMNAME = @FormName " +
                    //    " WHERE DBID = @dbId";
                //}

                try
                {
                    SqlCommand objCmd = Conn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = strSaveQry;

                    objCmd.Parameters.AddWithValue("@UserID", objUserRights.UserID);
                    objCmd.Parameters.AddWithValue("@MenuID", objUserRights.MenuID);
                    objCmd.Parameters.AddWithValue("@FormName", objUserRights.FormName);

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
        public static bool Delete(long userID)
        {
            int result = 0;

            using (SqlConnection Conn = new SqlConnection(General.GetSQLConnectionString()))
            {
                SqlCommand ObjDelCmd = Conn.CreateCommand();
                ObjDelCmd.CommandType = CommandType.Text;
                ObjDelCmd.CommandText = "DELETE FROM USERRIGHTS " +
                    " WHERE USERID = @userID";
                ObjDelCmd.Parameters.AddWithValue("@userID", userID);

                if (Conn.State != ConnectionState.Open)
                {
                    Conn.Open();
                }

                result = ObjDelCmd.ExecuteNonQuery();
                ObjDelCmd.Dispose();
            }
            return (result >= 0);
        }
    }
}
