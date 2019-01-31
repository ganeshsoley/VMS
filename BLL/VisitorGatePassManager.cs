using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Text;
using System.Transactions;
using EntityObject;
using DAL;

namespace BLL
{
    public class VisitorGatePassManager
    {
        /// <summary>
        /// Retrieves List of GatePasses availble.
        /// </summary>
        /// <param name="strWhere">Condition for filtering the list.</param>
        /// <returns>Collection Object GatePassList.</returns>
        public static VisitorGatePassList GetList(string strWhere, DateTime vwDate, bool flgShowAll)
        {
            return VisitorGatePassDAL.GetList(strWhere, vwDate, flgShowAll);
        }

        /// <summary>
        /// Retrieves VisitorGatePass Detail for Specified Record.
        /// </summary>
        /// <param name="dbid">Unique ID for Record in Database.</param>
        /// <returns>Object VisitorGatePass containing Data values.</returns>
        public static VisitorGatePass GetItem(int dbid)
        {
            return VisitorGatePassDAL.GetItem(dbid);
        }

        /// <summary>
        /// Saves current Object Values into Database.
        /// </summary>
        /// <param name="objVisitorGP">Current VisitorGP Object.</param>
        /// <returns>Boolean value True if record is saved successfully
        /// otherwise returns 'False' indicating record is not saved.</returns>
        public static bool Save(VisitorGatePass objVisitorGP, User objCurUser)
        {
            bool flgSave;
            try
            {
                using (TransactionScope objTScope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    if (objVisitorGP.IsEdited || objVisitorGP.IsNew)
                    {
                        //if (IsGatePassExist(objVisitorGP))
                        //{
                        //    throw new Exception("GatePass Already Exists.");
                        //}
                        //else
                        //{
                        VisitorGatePassDAL.Save(objVisitorGP, objCurUser);
                        if (objVisitorGP.IsNew)
                        {
                            VisitorGatePassDAL.CloseAppointment(objVisitorGP);
                        }
                        //}
                    }
                    flgSave = true;
                    objTScope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return flgSave;
        }

        /// <summary>
        /// Deletes Record from Database.
        /// </summary>
        /// <param name="objVisitorGP">Object containing all data values.</param>
        /// <returns>boolean value True if Record is deleted successfully 
        /// otherwise returns False.</returns>
        public static bool Delete(VisitorGatePass objVisitorGP)
        {
            bool recDel;
            recDel = VisitorGatePassDAL.Delete(objVisitorGP.DBID);
            if (recDel)
            {
                VisitorGatePassDAL.ReOpenAppointment(objVisitorGP.AppointmentNo);
            }
            return recDel;
        }

        /// <summary>
        /// Checks whether current Record already exists or not.
        /// </summary>
        /// <param name="objDept">Object containing all Data Values.</param>
        /// <returns>boolean value True if Record already Exists into Database
        /// otherwise returns False.</returns>
        public static bool IsGatePassExist(VisitorGatePass objVisitorGP)
        {
            return IsGatePassExist(objVisitorGP);
        }

        public static byte[] GetVisitorImage(long vDBID)
        {
            DataTable dTable;
            byte[] vImage = null;
            dTable = VisitorGatePassDAL.GetVisitorImage(vDBID);
            if (dTable.Rows.Count > 0)
            {
                vImage = (byte[])dTable.Rows[0]["VIMAGE"];
            }

            return vImage;
        }

        public static string[] GetEmployee()
        {
            DataTable dTable;
            string[] objEmplyeeList = null;
            dTable = VisitorGatePassDAL.GetEmployee();

            if (dTable.Rows.Count > 0)
            {
                objEmplyeeList = dTable.AsEnumerable().Select(row => row.Field<string>("TOMEET")).ToArray();
            }
            return objEmplyeeList;
        }
    }
}
