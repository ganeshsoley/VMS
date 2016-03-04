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
    public class ReturnableDCItemManager
    {
        private static DataTable dTable;

        /// <summary>
        /// Retrieves List of Items in DC availble.
        /// </summary>
        /// <param name="strWhere">Condition for filtering the list.</param>
        /// <returns>Collection Object DCItemList.</returns>
        public static ReturnableDCItemList GetList(long entryNo, DateTime entryDate, long masterDBID)
        {
            return ReturnableDCItemDAL.GetList(entryNo, entryDate, masterDBID);
        }

        /// <summary>
        /// Retrieves DCItem Detail for Specified Record.
        /// </summary>
        /// <param name="dbid">Unique ID for Record in Database.</param>
        /// <returns>Object DCItem containing Data values.</returns>
        public static ReturnableDCItem GetItem(int dbid)
        {
            return ReturnableDCItemDAL.GetItem(dbid);
        }

        /// <summary>
        /// Saves current Object Values into Database.
        /// </summary>
        /// <param name="objRetDC">Current Department Object.</param>
        /// <returns>Boolean value True if record is saved successfully
        /// otherwise returns 'False' indicating record is not saved.</returns>
        public static bool Save(ReturnableDCItem objDCItem)
        {
            bool flgSave = false;
            //flgSave = ReturnableDCItemDAL.Save(objDCItem);
            return flgSave;
        }

        /// <summary>
        /// This Method Deletes record from Database.
        /// </summary>
        /// <param name="objDCItem">Object which is to be Deleted.</param>
        /// <returns>Returns boolean value True if record is deleted Successfully
        /// otherwise returns false indicating that record is not deleted.</returns>
        public static bool Delete(ReturnableDCItem objDCItem)
        {
            bool recDAL;
            recDAL = ReturnableDCItemDAL.Delete(objDCItem.DBID);
            return recDAL;
        }

        public static string[] GetItems()
        {
            string[] objItemList = null;
            dTable = ReturnableDCItemDAL.GetItems();

            if (dTable.Rows.Count > 0)
            {
                objItemList = dTable.AsEnumerable().Select(row => row.Field<string>("ITEMCODE")).ToArray();
            }
            return objItemList;
        }

        public static string[] GetUnits()
        {
            string[] objUnitList = null;
            dTable = ReturnableDCItemDAL.GetUnits();

            if (dTable.Rows.Count > 0)
            {
                objUnitList = dTable.AsEnumerable().Select(row => row.Field<string>("UNIT")).ToArray();
            }
            return objUnitList;
        }
    }
}
