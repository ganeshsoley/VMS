using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using EntityObject;
using DAL;

namespace BLL
{
    public class DeptManager
    {
        /// <summary>
        /// Retrieves List of Departments availble.
        /// </summary>
        /// <param name="strWhere">Condition for filtering the list.</param>
        /// <returns>Collection Object DepartmentList.</returns>
        public static DepartmentList GetList(string strWhere)
        {
            return DeptDAL.GetList(strWhere);
        }

        /// <summary>
        /// Retrieves Department Detail for Specified Record.
        /// </summary>
        /// <param name="dbid">Unique ID for Record in Database.</param>
        /// <returns>Object Department containing Data values.</returns>
        public static Department GetItem(int dbid)
        {
            return DeptDAL.GetItem(dbid);
        }

        /// <summary>
        /// Saves current Object Values into Database.
        /// </summary>
        /// <param name="objDept">Current Department Object.</param>
        /// <returns>Boolean value True if record is saved successfully
        /// otherwise returns 'False' indicating record is not saved.</returns>
        public static bool Save(Department objDept)
        {
            bool flgSave;
            try
            {
                using (TransactionScope objTScope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    if (objDept.IsEdited || objDept.IsNew)
                    {
                        if (IsDeptExist(objDept))
                        {
                            throw new Exception("Dept. Already Exists.");
                        }
                        else
                        {
                            DeptDAL.Save(objDept);
                        }
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
        public static bool Delete(Department objDept)
        {
            bool recDel;
            recDel = DeptDAL.Delete(objDept.DBID);
            return recDel;
        }

        /// <summary>
        /// Checks whether current Record already exists or not.
        /// </summary>
        /// <param name="objDept">Object containing all Data Values.</param>
        /// <returns>boolean value True if Record already Exists into Database
        /// otherwise returns False.</returns>
        public static bool IsDeptExist(Department objDept)
        {
            return DeptDAL.IsDeptExist(objDept);
        }
    }
}
