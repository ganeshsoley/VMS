using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using EntityObject;
using DAL;

namespace BLL
{
    public class EmployeeManager
    {
        /// <summary>
        /// Retrieves Employee List from Database.
        /// </summary>
        /// <param name="strWhere">Condition based on which List is retrieved.</param>
        /// <returns>Employee List</returns>
        public static EmployeeList GetList(string strWhere)
        {
            return EmployeeDAL.GetList(strWhere);
        }

        /// <summary>
        /// Retrieves Employee Data from Database.
        /// </summary>
        /// <param name="dbid"></param>
        /// <returns></returns>
        public static Employee GetItem(long dbid)
        {
            return EmployeeDAL.GetItem(dbid);
        }

        /// <summary>
        /// Save Object Data into Database.
        /// </summary>
        /// <param name="objEmp">Employee Object containing property values.</param>
        /// <returns>Employee Object to be Saved.</returns>
        public static bool Save(Employee objEmp, User objUser)
        {
            bool flgSave;
            try
            {
                using (TransactionScope objTScope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    if (objEmp.IsEdited || objEmp.IsNew)
                    {
                        EmployeeDAL.Save(objEmp, objUser);
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
        public static bool Delete(Employee objEmp)
        {
            bool recDel;
            recDel = EmployeeDAL.Delete(objEmp.DBID);
            return recDel;
        }

        /// <summary>
        /// Checks whether current Record already exists or not.
        /// </summary>
        /// <param name="objEmp">Object containing all Data Values.</param>
        /// <returns>boolean value True if Record already Exists into Database
        /// otherwise returns False.</returns>
        public static bool IsEmployeeExist(Employee objEmp)
        {
            return EmployeeDAL.IsEmployeeExist(objEmp);
        }

    }
}
