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
    public class ReturnableDCManager
    {
        private static DataTable dTable;

        /// <summary>
        /// Retrieves List of ReturnableDCs availble.
        /// </summary>
        /// <param name="strWhere">Condition for filtering the list.</param>
        /// <returns>Collection Object ReturnableDCList.</returns>
        public static ReturnableDCList GetList(string entryType, DateTime lvDate, bool flgShowAll)
        {
            return ReturnableDCDAL.GetList(entryType, lvDate, flgShowAll);
        }

        /// <summary>
        /// Retrieves ReturnableDC Detail for Specified Record.
        /// </summary>
        /// <param name="dbid">Unique ID for Record in Database.</param>
        /// <returns>Object ReturnableDC containing Data values.</returns>
        public static ReturnableDC GetItem(int dbid)
        {
            return GetItem(dbid, false);
        }

        /// <summary>
        /// Retrieves ReturnableDC Detail for Specified Record.
        /// </summary>
        /// <param name="dbid"></param>
        /// <returns></returns>
        public static ReturnableDC GetItem(int dbid, bool flgWithItems)
        {
            ReturnableDC objDC = ReturnableDCDAL.GetItem(dbid);
            if (objDC != null && flgWithItems)
            {
                objDC.DCItems = ReturnableDCItemDAL.GetList(objDC.EntryNo, objDC.EntryDate, objDC.DBID);
            }
            return objDC;

        }

        /// <summary>
        /// Saves current Object Values into Database.
        /// </summary>
        /// <param name="objRetDC">Current Department Object.</param>
        /// <returns>Boolean value True if record is saved successfully
        /// otherwise returns 'False' indicating record is not saved.</returns>
        public static bool Save(ReturnableDC objRetDC)
        {
            bool flgSave;
                using (TransactionScope objTScope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    if (objRetDC.IsEdited || objRetDC.IsNew)
                    {
                        ReturnableDCDAL.Save(objRetDC);
                    }

                    if (objRetDC.DCItems != null)
                    {
                        foreach (ReturnableDCItem objItem in objRetDC.DCItems)
                        {
                            if (objItem.IsDeleted && !objItem.IsNew)
                            {
                                ReturnableDCItemDAL.Delete(objItem.DBID);
                            }
                            else if((objItem.IsEdited || objItem.IsNew) && !objItem.IsDeleted)
                            {
                                objItem.EntryNo = objRetDC.EntryNo;
                                objItem.EntryDate = objRetDC.EntryDate;
                                objItem.EntryType = objRetDC.EntryType;
                                objItem.MasterDBID = objRetDC.DBID;
                                objItem.DCNo = objRetDC.DCNo;
                                objItem.DCDate = objRetDC.DCDate;

                                ReturnableDCItemDAL.Save(objItem);
                            }
                        }
                    }
                    flgSave = true;
                    objTScope.Complete();
                }
            return flgSave;
        }

        /// <summary>
        /// Deletes Record from Database.
        /// </summary>
        /// <param name="objRetDC">Object containing all data values.</param>
        /// <returns>boolean value True if Record is deleted successfully 
        /// otherwise returns False.</returns>
        public static bool Delete(ReturnableDC objRetDC)
        {
            bool recDel;
            recDel = ReturnableDCDAL.Delete(objRetDC.DBID);
            return recDel;
        }

        public static string[] GetPartys()
        {
            string[] objPartyList = null;
            dTable = ReturnableDCDAL.GetPartys();

            if (dTable.Rows.Count > 0)
            {
                objPartyList = dTable.AsEnumerable().Select(row => row.Field<string>("PARTYNAME")).ToArray();
            }
            return objPartyList;
        }

        public static string[] GetVehicles()
        {
            string[] objVehicleList = null;
            dTable = ReturnableDCDAL.GetVehicles();

            if (dTable.Rows.Count > 0)
            {
                objVehicleList = dTable.AsEnumerable().Select(row => row.Field<string>("VEHICLENO")).ToArray();
            }
            return objVehicleList;
        }

        public static long GetEntryNo()
        {
            return ReturnableDCDAL.GetEntryNo();
        }
    }
}
