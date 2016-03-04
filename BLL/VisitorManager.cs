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
    public class VisitorManager
    {
        private static DataTable dTable;
        /// <summary>
        /// Retrieves Visitor List from Database.
        /// </summary>
        /// <param name="strWhere">Condition based on which List is retrieved.</param>
        /// <returns>Visitor List</returns>
        public static VisitorList GetList(string strCompany, string strContactNo)
        {
            return VisitorDAL.GetList(strCompany, strContactNo);
        }

        /// <summary>
        /// Retrieves Visitor Data from Database.
        /// </summary>
        /// <param name="dbid">unique id to identify record into database.</param>
        /// <returns>Visitor Object containing Data Values.</returns>
        public static Visitor GetItem(int dbid)
        {
            return VisitorDAL.GetItem(dbid);
        }

        /// <summary>
        /// Save Object Data into Database.
        /// </summary>
        /// <param name="objVisitor">Visitor Object containing property values.</param>
        /// <returns>Visitor Object to be Saved.</returns>
        public static bool Save(Visitor objVisitor)
        {
            bool flgSave;
            try
            {
                using (TransactionScope objTScope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    if (objVisitor.IsEdited || objVisitor.IsNew)
                    {
                        VisitorDAL.Save(objVisitor);
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
        public static bool Delete(Visitor objVisitor)
        {
            bool recDel;
            recDel = VisitorDAL.Delete(objVisitor.DBID);
            return recDel;
        }

        /// <summary>
        /// Checks whether current Record already exists or not.
        /// </summary>
        /// <param name="objVisitor">Object containing all Data Values.</param>
        /// <returns>boolean value True if Record already Exists into Database
        /// otherwise returns False.</returns>
        public static bool IsVisitorExist(Visitor objVisitor)
        {
            //return VisitorDAL.IsVisitorExist(objVisitor);
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public static List<string> GetCompanys()
        //{
        //    return VisitorDAL.GetCompanys();
        //}

        public static string[] GetCompanys()
        {
            string[] objCompanyList = null;
            dTable = VisitorDAL.GetCompanys();

            if (dTable.Rows.Count > 0)
            {
                objCompanyList = dTable.AsEnumerable().Select(row => row.Field<string>("COMPANY")).ToArray();
            }
            return objCompanyList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public static List<string> GetContacts()
        //{
        //    return VisitorDAL.GetContacts();
        //}

        public static string[] GetContacts()
        {
            string[] objContactList = null;
            dTable = VisitorDAL.GetContacts();

            if (dTable.Rows.Count > 0)
            {
                objContactList = dTable.AsEnumerable().Select(row => row.Field<string>("CONTACTNO")).ToArray();
            }
            return objContactList;
        }

        public static int GetVRegNo()
        {
            return VisitorDAL.GetVRegNo();
        }
    }
}
