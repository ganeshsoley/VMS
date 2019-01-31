using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using EntityObject;
using DAL;

namespace BLL
{
   public class DriverManager
    {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="strWhere"></param>
            /// <returns></returns>
            public static DriverList GetList(string strWhere)
            {
                return DriverDAL.GetList(strWhere);
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbid"></param>
        /// <returns></returns>
        public static Driver GetItem(int dbid)
        {
            return DriverDAL.GetItem(dbid);
        }

        /// <summary>
        /// Saves Current Object Values into Database.
        /// </summary>
        /// <param name="objDriver">Object containing data values.</param>
        /// <returns>Boolean value True if record is saved successfully
        /// otherwise returns False indicating record is not saved.</returns>
        public static bool Save(Driver objDriver, User objUser)
        {
            bool flgSave;
            try
            {
                using (TransactionScope objTScope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    if (objDriver.IsEdited || objDriver.IsNew)
                    {
                        DriverDAL.Save(objDriver, objUser);
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
        /// <param name="objDriver">Object containing all data values.</param>
        /// <returns>boolean value True if Record is deleted successfully 
        /// otherwise returns False.</returns>
        public static bool Delete(Driver objDriver)
        {
            bool recDel;
            recDel = DriverDAL.Delete(objDriver.DBID);
            return recDel;
        }

        /// <summary>
        /// Checks whether current Record already exists or not.
        /// </summary>
        /// <param name="objDriver">Object containing all Data Values.</param>
        /// <returns>boolean value True if Record already Exists into Database
        /// otherwise returns False.</returns>
        public static bool IsDriverExist(Driver objDriver)
        {
            return DriverDAL.IsDriverExist(objDriver);
        }

    }
}
