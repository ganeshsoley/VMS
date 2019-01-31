using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using EntityObject;
using DAL;
using System.Data;

namespace BLL
{
   public class VehInOutManager
    {
        private static DataTable dTable;
        /// <summary>
        /// Retrieves VehInOut List from Database.
        /// </summary>
        /// <param name="strWhere">Condition based on which List is retrieved.</param>
        /// <returns>VehInOut List</returns>
        public static VehInOutList GetList(DateTime vwDate, bool flgShowAll, int EntryType)
        {
            return VehInOutDAL.GetList("", vwDate, flgShowAll, EntryType);
        }

        /// <summary>
        /// Retrieves VehInOut Data from Database.
        /// </summary>
        /// <param name="dbid">unique id to identify record into database.</param>
        /// <returns>VehInOut Object containing Data Values.</returns>
        public static VehInOut GetItem(int dbid)
        {
            return VehInOutDAL.GetItem(dbid);
        }

        /// <summary>
        /// Save Object Data into Database.
        /// </summary>
        /// <param name="objVisitor">VehInOut Object containing property values.</param>
        /// <returns>VehInOut Object to be Saved.</returns>
        public static bool Save(VehInOut objVehInOut, User objUser)
        {
            bool flgSave;
            try
            {
                using (TransactionScope objTScope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    if (objVehInOut.IsEdited || objVehInOut.IsNew)
                    {
                        VehInOutDAL.Save(objVehInOut, objUser);
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
        /// <param name="objVisitor">Object containing all data values.</param>
        /// <returns>boolean value True if Record is deleted successfully 
        /// otherwise returns False.</returns>
        public static bool Delete(VehInOut objVehInOut)
        {
            bool recDel;
            recDel = VehInOutDAL.Delete(objVehInOut.Dbid);
            return recDel;
        }

        /// <summary>
        /// Checks whether current Record already exists or not.
        /// </summary>
        /// <param name="objVisitor">Object containing all Data Values.</param>
        /// <returns>boolean value True if Record already Exists into Database
        /// otherwise returns False.</returns>
        public static bool IsVehInOutExist(VehInOut objVehInOut)
        {
            //return VehInOutDAL.IsVehInOutExist(objVehInOut);
            return false;
        }

        public static string[] GetDrivers()
        {
            string[] objDriverList = null;
            dTable = VehInOutDAL.GetDrivers();

            if (dTable.Rows.Count > 0)
            {
                objDriverList = dTable.AsEnumerable().Select(row => row.Field<string>("DriverName")).ToArray();
            }
            return objDriverList;
        }

        public static string[] GetVehicles()
        {
            string[] objVehicleList = null;
            dTable = VehInOutDAL.GetVehicles();

            if (dTable.Rows.Count > 0)
            {
                objVehicleList = dTable.AsEnumerable().Select(row => row.Field<string>("VehNo")).ToArray();
            }
            return objVehicleList;
        }
        public static string[] GetInVendors()
        {
            string[] objInVendorList = null;
            dTable = VehInOutDAL.GetInVendors();

            if (dTable.Rows.Count > 0)
            {
                objInVendorList = dTable.AsEnumerable().Select(row => row.Field<string>("VENDORIN")).ToArray();
            }
            return objInVendorList;
        }
        public static string[] GetOutVendors()
        {
            string[] objOutVendorList = null;
            dTable = VehInOutDAL.GetOutVendors();

            if (dTable.Rows.Count > 0)
            {
                objOutVendorList = dTable.AsEnumerable().Select(row => row.Field<string>("VENDOROUT")).ToArray();
            }
            return objOutVendorList;
        }
        public static string[] GetCities()
        {
            string[] objGetCityList = null;
            dTable = VehInOutDAL.GetCities();

            if (dTable.Rows.Count > 0)
            {
                objGetCityList = dTable.AsEnumerable().Select(row => row.Field<string>("CITYNAME")).ToArray();
            }
            return objGetCityList;
        }

    }


}
