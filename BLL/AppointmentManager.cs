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
    public class AppointmentManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public static AppointmentList GetList(string strCondition, string strAppointmentNo, string strName, bool flgClose)
        {
            return AppointmentDAL.GetList(strCondition, strAppointmentNo, strName, flgClose);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbid"></param>
        /// <returns></returns>
        public static Appointment GetItem(int dbid)
        {
            return AppointmentDAL.GetItem(dbid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPOType"></param>
        /// <returns></returns>
        public static bool Save(Appointment objAppoint, User objCurUser)
        {
            bool flgSave;
            try
            {
                using (TransactionScope objTScope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    if (objAppoint.IsEdited || objAppoint.IsNew)
                    {
                        AppointmentDAL.Save(objAppoint, objCurUser);
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
        /// <param name="objDept">Object containing all data values.</param>
        /// <returns>boolean value True if Record is deleted successfully 
        /// otherwise returns False.</returns>
        public static bool Delete(Appointment objAppoint)
        {
            bool recDel;
            recDel = AppointmentDAL.Delete(objAppoint.DBID);
            return recDel;
        }

        /// <summary>
        /// Checks whether current Record already exists or not.
        /// </summary>
        /// <param name="objDept">Object containing all Data Values.</param>
        /// <returns>boolean value True if Record already Exists into Database
        /// otherwise returns False.</returns>
        public static bool IsAppointExist(Appointment objAppoint)
        {
            return AppointmentDAL.IsAppointExist(objAppoint);
        }

        public static string[] GetAppointments()
        {
            DataTable dTable = null;
            string[] objAppointmentList = null;
            dTable = AppointmentDAL.GetAppointments();

            if (dTable.Rows.Count > 0)
            {
                objAppointmentList = dTable.AsEnumerable().Select(row => row.Field<string>("APPOINTMENTNO")).ToArray();
            }
            return objAppointmentList;
        }

        public static string[] GetNames()
        {
            DataTable dTable = null;
            string[] objNamesList = null;
            dTable = AppointmentDAL.GetNames();

            if (dTable.Rows.Count > 0)
            {
                objNamesList = dTable.AsEnumerable().Select(row => row.Field<string>("NAME")).ToArray();
            }
            return objNamesList;
        }
    }
}
